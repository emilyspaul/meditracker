using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediTracker.Business
{
    public static class BoolExtensions
    {
        public static string GetYesNo(this bool value)
        {
            return value ? "Yes" : "No";
        }
    }
}
