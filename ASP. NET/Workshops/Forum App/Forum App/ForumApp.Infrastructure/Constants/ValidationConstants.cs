using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Infrastructure.Constants
{
    /// <summary>
    /// Validation Constants
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Maximal Post Title Length
        /// </summary>
        public const int TitleMaxLength = 50;

        /// <summary>
        /// Minimul Post Title Length
        /// </summary>
        public const int TitleMinLength = 10;

        /// <summary>
        /// Maximal Post Content Length
        /// </summary>
        public const int ContentMaxLength = 1500;

        /// <summary>
        /// Minimul Post Content Length
        /// </summary>
        public const int ContentMinLength = 30;
    }
}
