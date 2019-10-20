using imp_exp;
using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using omp_sepo.dialogs;
using omp_sepo.views;
using Oracle.DataAccess.Client;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ui_lib;

namespace omp_sepo
{
    public class TreeTaskView : TreeView
    {
        public void Init()
        {
            ImageList images = new ImageList();
            images.Images.Add("folder", Properties.Resources.folder);
            images.Images.Add("task", Properties.Resources.file);

            this.ImageList = images;

            this.NodeMouseClick += TreeTaskView_NodeMouseClick;
        }

        private void TreeTaskView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeTaskNode node = e.Node as TreeTaskNode;
            if (node.Type == TreeTaskNodeType.File)
            {
                int id = (int)node.Tag;
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
                        //var objects = obj_lib.Module.OpenSession()
                        //    .GetNamedQuery("TestHib")
                        //    .SetParameter(0, 1)
                        //    .SetParameter(1, "2")
                        //    .List();
                        //.List<TEST_HIB>();

                        (new FixtureAttachFilesDialog()).ShowDialog();
                        break;

                    case 11:
                        (new TbInstructionsDialog()).ShowDialog();
                        break;

                    case 12:
#if DEBUG
                        using (OracleTransaction transaction = obj_lib.Module.Connection.BeginTransaction())
                        {
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
                        //cmd.Connection = obj_lib.Module.Connection;
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
                        //cmd.Connection = obj_lib.Module.Connection;
                        //cmd.CommandText = "update techproc_comment set remark = :remark";

                        //FileStream fl = new FileStream("D:\\sepotpcmt.rtf", FileMode.Open);
                        //byte[] b = new byte[fl.Length];
                        //fl.Read(b, 0, b.Length);

                        //cmd.Parameters.Add(new OracleParameter("remark", b));
                        //cmd.ExecuteNonQuery();

                        //OracleCommand cmd = new OracleCommand();
                        //cmd.Connection = obj_lib.Module.Connection;
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
                        bool isrun = false;

                        // проверить, задана ли связь технологических операций
                        if (Module.OpenSession().QueryOver<SEPO_TECH_OPER_LINKS>().RowCount() > 0)
                        {
                            isrun = true;
                        }
                        else
                        {
                            if (MessageBox.Show(
                                "Не задано соответствие технологических операций. Уверены, что хотите продолжить?",
                                "Внимание!",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                isrun = true;
                            }
                        }

                        if (isrun)
                        {
                            (new TpImportDialog()).ShowDialog();
                        }

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

                    case 24:
                        (new AttachFilesUpdateHashDialog()).ShowDialog();
                        break;

                    case 25:
                        LoadFixtureDialog loadFixtureDialog = new LoadFixtureDialog();

                        if (loadFixtureDialog.ShowDialog() == DialogResult.OK)
                        {
                            LoadFixtureView fixture_view = new LoadFixtureView(loadFixtureDialog.Sign);
                            ((IMdiForm)Parent).AddChild("Загруженная оснастка", fixture_view, true);
                        }

                        break;

                    case 26:
                        TpImportOperationsView opersView = new TpImportOperationsView();
                        ((IMdiForm)Parent).AddChild("Технологические операции", opersView, true);

                        break;

                    case 27:
                        FixtureAttachFileObjects af_view = new FixtureAttachFileObjects();
                        ((IMdiForm)Parent).AddChild("Объекты для загрузки файлов", af_view, true);

                        break;

                    case 28:
                        TpImportOwnersView ownersView = new TpImportOwnersView();
                        ((IMdiForm)Parent).AddChild("Владельцы", ownersView, true);

                        break;

                    case 29:
                        TPManager mgr = new TPManager();

                        DialogResult dr = MessageBox.Show("Будут обновлены тексты переходов в ТП. Продолжить?", "Внимание!",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                        if (dr == DialogResult.OK)
                        {
                            try
                            {
                                mgr.StepTextToRTF();

                                MessageBox.Show("Обновление завершено!", "Информация",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show(exc.Message, "Ошибка!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public void LoadData(
            IViewRepository<SEPO_TASK_FOLDER_LIST> foldersRepo,
            IViewRepository<SEPO_TASK_LIST> tasksRepo,
            TreeNode node = null,
            int parent = 0)
        {
            var folders = foldersRepo.GetQuery()
                .Where(x => ((x.ID_PARENT == null) ? 0 : x.ID_PARENT.ID) == parent)
                .OrderBy(x => x.ID);

            foreach (var folder in folders)
            {
                TreeTaskNode childNode = new TreeTaskNode(folder.NAME, TreeTaskNodeType.Folder);
                childNode.Tag = folder.ID;
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

                LoadData(foldersRepo, tasksRepo, childNode, folder.ID);
            }

            var tasks = tasksRepo.GetQuery()
                .Where(x => x.ID_FOLDER.ID == parent)
                .OrderBy(x => x.ID);

            foreach (var task in tasks)
            {
                TreeTaskNode taskNode = new TreeTaskNode(task.NAME, TreeTaskNodeType.File);
                taskNode.Tag = task.ID;
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