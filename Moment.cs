using System;

namespace CS_TimeUtils
{
    public static class Moment
    {
        private static TimeframeCount getTimeframeCountToDate(DateTime end, DateTime currentTime) {
            TimeSpan difference = end - currentTime;

            int years = ((int)difference.TotalDays / 365);
            if (years >= 10) {
                return new TimeframeCount(Timeframe.Decades, (int)Math.Floor(years / 10d));
            } 
            
            if (years >= 1) {
                return new TimeframeCount(Timeframe.Years, years);
            }

            int days = (int)difference.TotalDays;
            if (days >= 1) {
                return new TimeframeCount(Timeframe.Days, days);
            }

            int hours = (int)difference.TotalHours;
            if (hours >= 1) {
                return new TimeframeCount(Timeframe.Hours, hours);
            }

            int minutes = (int)difference.TotalMinutes;
            if (minutes >= 1) {
                return new TimeframeCount(Timeframe.Minutes, minutes);
            }

            return new TimeframeCount(Timeframe.Seconds, (int)difference.Seconds);
        }

        private static string toFriendlyString(this Timeframe timeframe) {
            switch (timeframe) {
                case Timeframe.Decades:
                    return "decade";
                case Timeframe.Years:
                    return "year";
                case Timeframe.Months:
                    return "month";
                case Timeframe.Days:
                    return "day";
                case Timeframe.Hours:
                    return "hour";
                case Timeframe.Minutes:
                    return "minute";
                case Timeframe.Seconds:
                    return "second";
                default:
                    throw new Exception("Provided timeframe is invalid");
            }
        }

        private static string padIntWithLeadingZeros(int number, int leadingCount) {
            string numberString = number.ToString();
            int lengthDelta = leadingCount - numberString.Length;

            return new string('0', Math.Min(lengthDelta, 0)) + numberString;
        }

        public static string getTimeUntilString(DateTime end, DateTime currentTime) {
            return Moment.getTimeframeCountToDate(end, currentTime).ToString();
        }

        public static string getDigitalClockString(DateTime end, DateTime currentTime) {
            TimeSpan difference = end - currentTime;

            return $"{difference.Days}d {padIntWithLeadingZeros(difference.Hours, 2)}:{padIntWithLeadingZeros(difference.Minutes, 2)}:{padIntWithLeadingZeros(difference.Seconds, 2)}";
        }

        private enum Timeframe {
            Seconds,
            Minutes,
            Hours,
            Days,
            Months,
            Years,
            Decades,
        }

        private readonly struct TimeframeCount {
            private readonly Timeframe frame;
            private readonly int count;

            public TimeframeCount(Timeframe frame, int count) {
                this.frame = frame;
                this.count = count;
            }
            
            public override string ToString() {
                return $"{this.count} {this.frame.toFriendlyString()}{(this.count == 1 ? "" : "s")}";
            }
        }
    }
}
