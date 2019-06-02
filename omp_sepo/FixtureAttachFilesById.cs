using obj_lib;
using obj_lib.Entities;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Linq;

namespace omp_sepo
{
    public class FixtureAttachFilesById
    {
        private string currentFile;

        private int currentPosition;

        /// <summary>
        /// Начало операции
        /// </summary>
        public event EventHandler BeginRun;

        /// <summary>
        /// Окончание операции
        /// </summary>
        public event EventHandler EndRun;

        /// <summary>
        /// Происходит перед присоединением файла
        /// </summary>
        public event EventHandler<FixtureAttachFileStartArgs> BeginAttachFile;

        /// <summary>
        /// Происходит после присоединения файла
        /// </summary>
        public event EventHandler<FixtureAttachFileEndArgs> EndAttachFile;

        public bool Exit { get; set; }

        public string DirectoryFilePath { get; set; }

        public FixtureAttachFilesById()
        {
            currentFile = String.Empty;
            currentPosition = 0;
            Exit = false;
        }

        public FixtureAttachFilesById(string directory)
            : this()
        {
            DirectoryFilePath = directory;
        }

        public int CountFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryFilePath);
            return dirInfo.EnumerateFiles().Count();
        }

        public void Run()
        {
            EventArgs args = new EventArgs();
            BeginRun?.Invoke(this, args);

            DirectoryInfo directory = new DirectoryInfo(DirectoryFilePath);

            foreach (var file in directory.GetFiles())
            {
                if (Exit)
                {
                    return;
                }

                currentFile = file.Name;
                currentPosition++;

                FixtureAttachFileStartArgs beginAttachArgs = new FixtureAttachFileStartArgs(currentFile);
                BeginAttachFile?.Invoke(this, beginAttachArgs);

                FixtureAttachFileEndArgs endAttachArgs = new FixtureAttachFileEndArgs();
                endAttachArgs.FileName = file.Name;
                endAttachArgs.Objects = new List<FixtureAttachFileObject>();

                if (Regex.IsMatch(file.Name, @"^\d+"))
                {
                    int idDoc = Convert.ToInt32(Regex.Match(file.Name, @"^\d+").Value);

                    // файл
                    FileStream stream = File.OpenRead(file.FullName);

                    byte[] bytes = new byte[file.Length];
                    stream.Read(bytes, 0, (int)file.Length);

                    // контрольная сумма
                    SHA1 s = new SHA1CryptoServiceProvider();
                    byte[] hash = s.ComputeHash(bytes);

                    string hashStr = (System.BitConverter.ToString(hash)).Replace("-", "").ToLower();

                    // транзакция
                    OracleConnection connection = obj_lib.Module.OpenSession().Connection as OracleConnection;

                    OracleCommand command = new OracleCommand("pkg_sepo_import_global.attach_fixture_file");
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    OracleParameter p_result = new OracleParameter(
                        "p_result", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

                    OracleParameter p_iddoc = new OracleParameter("p_iddoc", idDoc);
                    OracleParameter p_docname = new OracleParameter("p_docname", file.Name);
                    OracleParameter p_hash = new OracleParameter("p_hash", hashStr);
                    OracleParameter p_data = new OracleParameter("p_data", OracleDbType.Blob);

                    command.Parameters.AddRange(new OracleParameter[] { p_result, p_iddoc, p_docname, p_hash, p_data });

                    p_data.Value = bytes;
                    command.ExecuteNonQuery();

                    OracleRefCursor result = (OracleRefCursor)p_result.Value;

                    using (OracleDataReader rd = result.GetDataReader())
                    {
                        while (rd.Read())
                        {
                            FixtureAttachFileObject obj = new FixtureAttachFileObject();
                            obj.Sign = rd.GetString(3);
                            obj.State = (rd.GetInt16(4) == 1) ? FixtureAttachFileState.Success : FixtureAttachFileState.None;

                            endAttachArgs.Objects.Add(obj);
                        }
                    }

                    //var result = obj_lib.Module.OpenSession()
                    //    .GetNamedQuery("AttachFixtureFile")
                    //    .SetParameter(0, idDoc)
                    //    .SetParameter(1, file.Name)
                    //    .SetParameter(2, hashStr)
                    //    .SetParameter(3, bytes)
                    //    .List<SEPO_FIXTURE_AF_RESULT_TEMP>();

                    //transaction.Commit();

                    //foreach (var pos in result)
                    //{
                    //    FixtureAttachFileObject obj = new FixtureAttachFileObject();
                    //    obj.Sign = pos.OBJSIGN;
                    //    obj.State = (pos.STATE == 1) ? FixtureAttachFileState.Success : FixtureAttachFileState.None;

                    //    endAttachArgs.Objects.Add(obj);
                    //}
                }

                EndAttachFile?.Invoke(this, endAttachArgs);
            }

            System.Threading.Thread.Sleep(1500);

            EndRun?.Invoke(this, args);
        }
    }
}