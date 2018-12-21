namespace RegexHelper
{
    using System.Text.RegularExpressions;
    using System.Windows;

    public class MainModel : DependencyObject
    {
        // Using a DependencyProperty as the backing store for BaseString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BaseStringProperty =
            DependencyProperty.Register("BaseString", typeof(string), typeof(MainModel), new PropertyMetadata(string.Empty));

        // Using a DependencyProperty as the backing store for RegexString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegexStringProperty =
            DependencyProperty.Register("RegexString", typeof(string), typeof(MainModel), new PropertyMetadata(string.Empty));

        public string BaseString
        {
            get => (string)this.GetValue(BaseStringProperty);
            set => this.SetValue(BaseStringProperty, value);
        }

        public string RegexString
        {
            get => (string)this.GetValue(RegexStringProperty);
            set => this.SetValue(RegexStringProperty, value);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == BaseStringProperty)
            {
                var escapeChars = @"\.$^{[(|)*+?".ToCharArray();
                var newRegexString = (string)e.NewValue;
                foreach (var escapeChar in escapeChars)
                {
                    newRegexString = newRegexString.Replace($"{escapeChar}", $@"\{escapeChar}");
                }

                newRegexString = Regex.Replace(newRegexString, @"\s+", @"[\s\n]*");

                this.RegexString = newRegexString;
            }
        }
    }
}