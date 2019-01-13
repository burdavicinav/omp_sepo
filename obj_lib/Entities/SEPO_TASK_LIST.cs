namespace obj_lib.Entities
{
    public class SEPO_TASK_LIST
    {
        public virtual int ID { get; protected set; }

        public virtual string NAME { get; set; }

        public virtual SEPO_TASK_FOLDER_LIST ID_FOLDER { get; set; }
    }
}