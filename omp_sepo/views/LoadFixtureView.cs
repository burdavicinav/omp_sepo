using obj_lib;
using obj_lib.Entities;
using obj_lib.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class LoadFixtureView : UserControl
    {
        private IViewRepository<V_SEPO_FIXTURE_DOCS> _docsRepo;

        private IRepository<SEPO_OSN_ALL> _objRepo;

        private string _sign;

        public LoadFixtureView(IViewRepository<V_SEPO_FIXTURE_DOCS> docsRepo, IRepository<SEPO_OSN_ALL> objRepo, string sign)
        {
            _docsRepo = docsRepo;
            _objRepo = objRepo;
            _sign = sign;

            InitializeComponent();

            var query = _docsRepo.GetQuery();

            if (_sign != String.Empty)
            {
                query = query.Where(x => x.NAME == _sign);
            }

            query = query.OrderBy(x => x.NAME);

            foreach (var obj in query)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Tag = obj.ART_ID;

                row.CreateCells(
                    scene,
                    obj.ART_ID,
                    obj.DOC_ID,
                    obj.NAME,
                    obj.REVISION,
                    obj.STATE,
                    obj.FILENAME,
                    obj.FILE_ISLOAD);

                scene.Rows.Add(row);
            }

            scene.CellEndEdit += Scene_CellEndEdit;
        }

        private void Scene_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = scene.Rows[e.RowIndex];

            decimal art_id = (decimal)row.Tag;

            decimal isload = ((bool)row.Cells[6].Value) ? 1 : 0;

            using (UnitOfWork transaction = new UnitOfWork())
            {
                try
                {
                    SEPO_OSN_ALL obj = _objRepo.GetQuery().Where(x => x.ART_ID == art_id).FirstOrDefault();

                    if (obj != null)
                    {
                        obj.FILE_ISLOAD = isload;

                        _objRepo.Update(obj);
                    }

                    transaction.Commit();

                    decimal art_id_row = 0;

                    foreach (DataGridViewRow r in scene.Rows)
                    {
                        art_id_row = (decimal)r.Tag;

                        if (art_id_row == art_id)
                        {
                            r.Cells[6].Value = isload;
                        }
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public LoadFixtureView(string sign) :
            this(new ViewRepository<V_SEPO_FIXTURE_DOCS>(), new Repository<SEPO_OSN_ALL>(), sign)
        { }
    }
}