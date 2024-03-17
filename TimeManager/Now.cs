using System;

namespace CSharpToolKit.TimeManager
{
    public class Now
    {
        private DateTime _current { get; set; }

        /// <summary>
        /// Retrieves the current date in the specified format.
        /// </summary>
        /// <param name="id">Optional. Specifies the format for the date.
        /// <list type="table">
        /// <item><term>0</term><description>dd/MM/yyyy</description></item>
        /// <item><term>1</term><description>yyyyMMdd</description></item>
        /// <item><term>-1</term><description>yyyy_MM_dd</description></item>
        /// <item><term>2</term><description>MMddyyyy</description></item>
        /// <item><term>-2</term><description>MM_dd_yyyy</description></item>
        /// <item><term>3</term><description>ddMMyyyy</description></item>
        /// <item><term>-3</term><description>dd_MM_yyyy</description></item>
        /// <item><term>Default</term><description>yyyyMMdd</description></item>
        /// </list>
        /// </param>
        /// <returns>The current date.</returns>
        public string Date(int id = 1)
        {
            string date;
            _current = System.DateTime.Now;

            switch (id)
            {
                case 0:
                    date = _current.ToString("dd/MM/yyyy");
                    break;
                case 1:
                default:
                    date = _current.ToString("yyyyMMdd");
                    break;
                case -1:
                    date = _current.ToString("yyyy_MM_dd");
                    break;
                case 2:
                    date = _current.ToString("MMddyyyy");
                    break;
                case -2:
                    date = _current.ToString("MM_dd_yyyy");
                    break;
                case 3:
                    date = _current.ToString("ddMMyyyy");
                    break;
                case -3:
                    date = _current.ToString("dd_MM_yyyy");
                    break;
            }

            return date;
        }

        /// <summary>
        /// Retrieves the current time in the specified format.
        /// </summary>
        /// <param name="id">Optional. Specifies the format for the time.
        /// <list type="table">
        /// <item><term>0</term><description>HH:mm:ss</description></item>
        /// <item><term>1</term><description>HHmmss</description></item>
        /// <item><term>-1</term><description>HH_mm_ss</description></item>
        /// <item><term>Default</term><description>HHmmss</description></item>
        /// </list>
        /// </param>
        /// <returns>The current time.</returns>
        public string Time(int id = 1)
        {
            string time;
            _current = System.DateTime.Now;

            switch (id)
            {
                case 0:
                    time = _current.ToString("HH:mm:ss");
                    break;
                case 1:
                default:
                    time = _current.ToString("HHmmss");
                    break;
                case -1:
                    time = _current.ToString("HH_mm_ss");
                    break;
            }

            return time;
        }

        /// <summary>
        /// Retrieves the current date and time concatenated together in the specified order and format.
        /// </summary>
        /// <param name="dateId">Specifies the format for the date part.
        /// /// <list type="table">
        /// <item><term>0</term><description>dd/MM/yyyy</description></item>
        /// <item><term>1</term><description>yyyyMMdd</description></item>
        /// <item><term>-1</term><description>yyyy_MM_dd</description></item>
        /// <item><term>2</term><description>MMddyyyy</description></item>
        /// <item><term>-2</term><description>MM_dd_yyyy</description></item>
        /// <item><term>3</term><description>ddMMyyyy</description></item>
        /// <item><term>-3</term><description>dd_MM_yyyy</description></item>
        /// <item><term>Default</term><description>yyyyMMdd</description></item>
        /// </list>
        /// </param>
        /// <param name="timeId">Specifies the format for the time part.
        /// /// <list type="table">
        /// <item><term>0</term><description>HH:mm:ss</description></item>
        /// <item><term>1</term><description>HHmmss</description></item>
        /// <item><term>-1</term><description>HH_mm_ss</description></item>
        /// <item><term>Default</term><description>HHmmss</description></item>
        /// </list>
        /// </param>
        /// <param name="orderId">Specifies the order and format in which date and time are concatenated.
        /// <list type="table">
        /// <item><term>0</term><description>date time</description></item>
        /// <item><term>1</term><description>datetime</description></item>
        /// <item><term>-1</term><description>date_time</description></item>
        /// <item><term>2</term><description>time date</description></item>
        /// <item><term>-2</term><description>time_date</description></item>
        /// <item><term>Default</term><description>datetime</description></item>
        /// </list>
        /// </param>
        /// <returns>The current date and time in the specified order and format.</returns>
        public string DateTime(int dateId = 1, int timeId = 1, int orderId = 1)
        {
            string now;
            string date = Date(dateId);
            string time = Time(timeId);

            switch (orderId)
            {
                case 0:
                    now = $"{date}  {time}";
                    break;
                case 1:
                default:
                    now = $"{date}{time}";
                    break;
                case -1:
                    now = $"{date}_{time}";
                    break;
                case 2:
                    now = $"{time}{date}";
                    break;
                case -2:
                    now = $"{time}_{date}";
                    break;
            }

            return now;
        }
    }
}
