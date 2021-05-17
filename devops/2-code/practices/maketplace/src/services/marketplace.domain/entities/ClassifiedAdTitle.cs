using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using marketplace.domain.kernal;

namespace marketplace.domain.entities
{
    public class ClassifiedAdTitle:ValueObject
    {
        public static ClassifiedAdTitle FromString(string title)=>new  ClassifiedAdTitle(title);

        public static ClassifiedAdTitle FromHtml(string htmlTitle)
        {
            var supportedTagsReplaced = htmlTitle
                .Replace("<i>", "*")
                .Replace("</i>", "*")
                .Replace("<b>", "*")
                .Replace("</b>", "*");
            return new ClassifiedAdTitle(Regex.Replace(supportedTagsReplaced, "<.*.?>", string.Empty));
        }
        private readonly string _value;

        private ClassifiedAdTitle(string value)
        {
            if(value.Length>100)
            {
                throw new ArgumentException("Title cannot be longer that 100 characters", nameof(value));
            }
            _value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}