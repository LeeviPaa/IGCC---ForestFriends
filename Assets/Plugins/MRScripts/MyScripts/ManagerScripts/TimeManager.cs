namespace MasujimaRyohei
{
    using UnityEngine;
    using System.Collections;

    public class TimeManager : SingletonMonoBehaviour<TimeManager>
    {
        #region [ VARIABLES ]

        private int _h, _m, _s;
        private int _increaseTime = 0;
        private int _reduceTime = 0;
        private bool _isUsingStopwatch = false;
        private bool _isUsingTimer = false;

        public int hour
        {
            get { return _h; }
            set { _h = value; }
        }
        public int minute
        {
            get { return _m; }
            set { _m = value; }
        }
        public int second
        {
            get { return _s; }
            set { _s = value; }
        }

        #endregion

     
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            string time = System.DateTime.Now.TimeOfDay.ToString();

            _h = int.Parse(time.Split(':')[0]);
            _m = int.Parse(time.Split(':')[1]);
            _s = (int)float.Parse(time.Split(':')[2]);
        }

        public int GetHour12()
        {
            int ct24;
            ct24 = _h;
            if (IsForenoon())
                return ct24;
            else
                return ct24 - 12;
        }
        public bool IsForenoon()
        {
            if (_h < 12)
                return true;
            else
                return false;
        }
        public bool IsAfternoon()
        {
            if (_h >= 12)
                return true;
            else
                return false;
        }

        public void StartStopwatch()
        {
            _isUsingStopwatch = true;
            StartCoroutine("CountUpTimer");
        }

        private IEnumerator CountUpTimer()
        {
            while (_isUsingStopwatch)
            {
                _increaseTime++;
                yield return new WaitForSeconds(1.0f);
            }
        }

        public void StopStopwatch()
        {
            StopCoroutine("CountUpTimer");
            _isUsingStopwatch = false;
        }

        int GetCurrentTimeOfStopwatch()
        {
            return _increaseTime;
        }

        bool SetTimer(int time)
        {
            _reduceTime = time;
            return false;
        }

        private IEnumerator CountDownTimer()
        {
            while (_isUsingTimer)
            {
                _reduceTime--;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}