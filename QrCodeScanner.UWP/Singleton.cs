namespace QrCodeScanner.UWP
{
    public class Singleton
    {
        private static Singleton _instance;

        public static Singleton Instance
        {
            get
            {
                if(_instance==null) _instance=new Singleton();
                return _instance;
            }
        }

        public bool CanScan { get; set; }
    }
}