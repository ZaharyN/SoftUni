using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class Route : IRoute
    {
        private string startPoint;
        private string endPoint;
        private double length;
        private int routeId;

        public Route(string startPoint, string endPoint, double length, int routeId)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Length = length;
            RouteId = routeId;
            IsLocked = false;
        }

        public string StartPoint
        {
            get => startPoint;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.StartPointNull));
                }
                startPoint = value;
            }
        }

        public string EndPoint
        {
            get => endPoint;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.EndPointNull));
                }
                endPoint = value;
            }
        }

        public double Length
        {
            get => length;
            private set
            {
                if(value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.RouteLengthLessThanOne));
                }
                length = value;
            }
        }

        public int RouteId
        {
            get => routeId;
            private set
            {
                routeId = value;
            }
        }

        public bool IsLocked
        {
            get; private set;
        }

        public void LockRoute() => IsLocked = true;
    }
}
