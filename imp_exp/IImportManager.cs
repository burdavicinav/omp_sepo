namespace imp_exp
{
    public interface IImportManager
    {
        void Load();

        bool VerifyXml(string file);

        void LoadFromXml(string file);

        void LoadFromCsv(string file);
    }
}