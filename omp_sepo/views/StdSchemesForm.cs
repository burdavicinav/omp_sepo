using System.Windows.Forms;

namespace omp_sepo.views
{
    public partial class StdSchemesForm : Form
    {
        public StdSchemesForm()
        {
            InitializeComponent();

            schemesView.Form = this;
            schemesView.ViewSettings();
            schemesView.MenuSettings();

            attrsView.ViewSettings();

            schemesView.ItemSelectionChanged += SchemesView_ItemSelectionChanged;
        }

        private void SchemesView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                decimal id_record = (decimal)e.Item.Tag;
                //attrsView.UpdateScene(id_record);
            }
        }

        public StdSchemesForm(decimal lvl, bool is_edit_items) : this()
        {
            schemesView.UpdateScene(lvl, is_edit_items);
        }
    }
}