using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebOficinaDOM.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return true;
            }
            return file.ContentLength <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(Math.Ceiling((_maxFileSize / 1024f) / 1024f).ToString());
        }
    }
}