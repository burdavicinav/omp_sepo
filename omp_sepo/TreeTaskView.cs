using imp_exp;
using omp_sepo.dialogs;
using omp_sepo.views;
using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;
using ui_lib;

namespace omp_sepo
{
    public class TreeTaskView : TreeView
    {
        public void Init()
        {
            //ImageList images = new ImageList();
            //images.Images.Add("folder", Properties.Resources.folder);
            //images.Images.Add("task", Properties.Resources.file);

            //this.ImageList = images;

            this.NodeMouseClick += TreeTaskView_NodeMouseClick;
        }

        private void TreeTaskView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeTaskNode node = e.Node as TreeTaskNode;
            if (node.Type == TreeTaskNodeType.File)
            {
                long id = (long)node.Tag;
                switch (id)
                {
                    case 1:
                        (new ProfessionsImportDialog()).ShowDialog();
                        break;

                    case 2:
                        (new OperationsImportDialog()).ShowDialog();
                        break;

                    case 3:
                        (new StepsImportDialog()).ShowDialog();
                        break;

                    case 4:
                        (new EquipmentModelsImport()).ShowDialog();
                        break;

                    case 5:
                        break;

                    case 6:
                        (new ProfessionsExportDialog()).ShowDialog();
                        break;

                    case 7:
                        (new FixtureImportFilesDialog()).ShowDialog();
                        break;

                    case 8:
                        OsnTypesListView view = new OsnTypesListView();
                        ((IMdiForm)Parent).AddChild("Типы оснастки", view);
                        break;

                    case 9:
                        (new FixtureImportObjectsDialog()).ShowDialog();
                        break;

                    case 10:
                        (new FixtureAttachFilesDialog()).ShowDialog();
                        break;

                    case 11:
                        (new TbInstructionsDialog()).ShowDialog();
                        break;

                    case 12:
#if DEBUG
                        using (OracleTransaction transaction = Module.Connection.BeginTransaction())
                        {
                            imp_exp.Module.Connection = Module.Connection;

                            try
                            {
                                StandardFixtureManager mng = new StandardFixtureManager();
                                mng.LoadFromXml(
                                    "data\\Оснастка.xml",
                                    "data\\Списки.xml"
                                    );

                                TPManager tp_mng = new TPManager();
                                tp_mng.LoadFromXml(
                                    "data\\_TP_20180208\\TpWithArts.xml",
                                    TPImportGroup.TP,
                                    "data\\_TP_20180208\\Entities.xml",
                                    "data\\_TP_20180208",
                                    "TC"
                                    );

                                transaction.Commit();

                                MessageBox.Show(
                                    "Операция успешно завершена!",
                                    "Информация",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                    );
                            }
                            catch (Exception exc)
                            {
                                transaction.Rollback();

                                MessageBox.Show(
                                    exc.Message + " " + exc.StackTrace,
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                            }
                        }
#else
                        (new StdFixtureImportDialog()).ShowDialog();
#endif
                        break;

                    case 13:
                        StdSchemesDialog dialog = new StdSchemesDialog();
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            StdSchemesForm schemes_view = new StdSchemesForm(
                                dialog.Lvl,
                                dialog.IsEditItems
                                );
                            ((IMdiForm)Parent).AddChild("Схемы атрибутов", schemes_view, true);
                        }

                        break;

                    case 14:
                        StdAttrsListView attrs_view = new StdAttrsListView();
                        attrs_view.ViewSettings();
                        attrs_view.UpdateScene();

                        ((IMdiForm)Parent).AddChild("Атрибуты", attrs_view, true);
                        break;

                    case 16:
                        if (MessageBox.Show(
                            "Вы уверены, что хотите обновить структуру БД?",
                            "Внимание", MessageBoxButtons.YesNo
                            ) == DialogResult.Yes
                            )
                        {
                            try
                            {
                                imp_exp.Module.Connection = Module.Connection;

                                StandardFixtureManager mng = new StandardFixtureManager();
                                mng.UpdateDB();

                                MessageBox.Show(
                                    "Операция успешно завершена!",
                                    "Информация",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                    );
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show(
                                    exc.Message + " " + exc.StackTrace,
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                            }
                        }
                        break;

                    case 15:
                        StdFoxProAttrsView foxattr_view = new StdFoxProAttrsView();
                        ((IMdiForm)Parent).AddChild("Атрибуты FoxPro", foxattr_view, true);

                        break;

                    case 18:
                        (new StdFixtureOmpImportDialog()).ShowDialog();
                        break;

                    case 19:
                        (new StdFixtureImportClassifyDialog()).ShowDialog();
                        break;

                    case 20:
#if DEBUG
                        //OracleCommand cmd = new OracleCommand();
                        //cmd.Connection = Module.Connection;
                        //cmd.CommandText = "select steptext_rtf from steps_for_oper_steptext where stepcode = 461";

                        //using (OracleDataReader rd = cmd.ExecuteReader())
                        //{
                        //    rd.Read();

                        //    Oracle.DataAccess.Types.OracleBlob bl = rd.GetOracleBlob(0);
                        //    byte[] b = bl.Value;

                        //    FileStream fl = new FileStream("D:\\tpcmt.rtf", FileMode.Create);
                        //    fl.Write(b, 0, b.Length);
                        //    fl.Close();
                        //}

                        //OracleCommand cmd = new OracleCommand();
                        //cmd.Connection = Module.Connection;
                        //cmd.CommandText = "update techproc_comment set remark = :remark";

                        //FileStream fl = new FileStream("D:\\sepotpcmt.rtf", FileMode.Open);
                        //byte[] b = new byte[fl.Length];
                        //fl.Read(b, 0, b.Length);

                        //cmd.Parameters.Add(new OracleParameter("remark", b));
                        //cmd.ExecuteNonQuery();

                        //OracleCommand cmd = new OracleCommand();
                        //cmd.Connection = Module.Connection;
                        //cmd.CommandText = "select rtf from sepo_tp_comment_blob where id = 2226406";

                        //using (OracleDataReader rd = cmd.ExecuteReader())
                        //{
                        //    rd.Read();

                        //    Oracle.DataAccess.Types.OracleBlob bl = rd.GetOracleBlob(0);
                        //    byte[] b = bl.Value;

                        //    FileStream fl = new FileStream("D:\\sepotpcmt.rtf", FileMode.Create);
                        //    fl.Write(b, 0, b.Length);
                        //    fl.Close();
                        //}
#endif
                        (new TpImportDialog()).ShowDialog();
                        break;

                    case 21:
                        TFlexSpecSectionsView sections_view = new TFlexSpecSectionsView();
                        ((IMdiForm)Parent).AddChild("Секции спецификации", sections_view, true);
                        break;

                    case 22:
                        TFlexSignDocsView sign_view = new TFlexSignDocsView();
                        ((IMdiForm)Parent).AddChild("Обозначения документов", sign_view, true);
                        break;

                    case 23:
                        TFlexObjSynchView objsynch_view = new TFlexObjSynchView();
                        ((IMdiForm)Parent).AddChild("Синхронизация объектов", objsynch_view, true);
                        break;

                    default:
                        break;
                }
            }
        }

        public void LoadData(TreeNode node = null, long parent = 0)
        {
            OracleCommand command = new OracleCommand();
            command.CommandText =
                "select * from sepo_task_folder_list where coalesce(id_parent, 0) = :parent";
            command.Connection = Module.Connection;
            command.Parameters.Add("parent", parent);

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                long id = reader.GetInt64(0);
                string name = reader.GetString(1);

                TreeTaskNode childNode = new TreeTaskNode(name, TreeTaskNodeType.Folder);
                childNode.Tag = id;
                childNode.ImageKey = "folder";
                childNode.SelectedImageKey = "folder";

                if (node == null)
                {
                    this.Nodes.Add(childNode);
                }
                else
                {
                    node.Nodes.Add(childNode);
                }

                LoadData(childNode, id);
            }

            OracleCommand task_command = new OracleCommand();
            task_command.CommandText =
                "select * from sepo_task_list where id_folder = :folder order by id";
            task_command.Connection = Module.Connection;
            task_command.Parameters.Add("folder", parent);

            OracleDataReader task_reader = task_command.ExecuteReader();
            while (task_reader.Read())
            {
                long id_task = task_reader.GetInt64(0);
                string name_task = task_reader.GetString(1);

                TreeTaskNode taskNode = new TreeTaskNode(name_task, TreeTaskNodeType.File);
                taskNode.Tag = id_task;
                taskNode.ImageKey = "task";
                taskNode.SelectedImageKey = "task";

                if (node == null)
                {
                    this.Nodes.Add(taskNode);
                }
                else
                {
                    node.Nodes.Add(taskNode);
                }
            }
        }

        public TreeTaskView()
        {
        }
    }
}