using System;

namespace MyJobDiary.View
{

    public class MasterPageItem
    {
        public MasterPageItem()
        {
            Page =  Pages.MainPage;
        }

        public string Title { get; set; }

        public string IconSource { get; set; }

        public Pages Page { get; set; }
    }
}