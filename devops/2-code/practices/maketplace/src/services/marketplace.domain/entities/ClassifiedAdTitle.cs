using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using marketplace.domain.kernel;

namespace marketplace.domain.entities
{
    public class ClassifiedAdTitle:ValueObject
    {
        public static ClassifiedAdTitle FromString(string title)
        {
            Validate(title);
            return new ClassifiedAdTitle(title);
        }

        public static ClassifiedAdTitle FromHtml(string htmlTitle)
        {
            var supportedTagsReplaced = htmlTitle
                .Replace("<i>", "*")
                .Replace("</i>", "*")
                .Replace("<b>", "*")
                .Replace("</b>", "*");
            var value = Regex.Replace(supportedTagsReplaced, "<.*.?>", string.Empty);
            Validate(value);
            return new ClassifiedAdTitle(value);
        }

        public static ClassifiedAdTitle NoTitle() => new ClassifiedAdTitle();
        public string Value { get; internal set;}

        protected ClassifiedAdTitle(){}
        private ClassifiedAdTitle(string value)=>Value = value;

        private static void Validate(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Title cannot be null or empty", nameof(value));
            }
            if(value.Length>100)
            {
                throw new ArgumentException("Title cannot be longer that 100 characters", nameof(value));
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator string (ClassifiedAdTitle title) => title.Value;
    }
}