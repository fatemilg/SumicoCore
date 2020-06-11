namespace SCMCore.ViewModel
{
    public class Search 
    {
        public string Filter { get; set; }
        public string Order { get; set; }
        public string JsonResult { get; set; }
        public Search()
        {
            Filter = Order = "";
        }
    }
}