namespace MyJobDiary.ViewModel
{
    public class MasterViewModel : ObservableObject
    {
        #region User

        private string _userName;
        private string _photo;

        public MasterViewModel(string userName, string photoUrl)
        {
            UserName = userName;
            Photo = photoUrl;
        }

        public string UserName
        {
            get => _userName;
            private set => SetField(ref _userName, value);
        }

        public string Photo
        {
            get => _photo;
            private set => SetField(ref _photo, value);
        }


        #endregion
    }
}
