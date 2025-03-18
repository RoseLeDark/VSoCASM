using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsocasm.CommandLine
{
    public class Argument
    {
        public object Value { get; set; }

        public virtual T GetValue<T>() => (T)Value;

        public virtual void SetValue<T>(T value)
        {
            Value = value;
        }

        public string HelpText => GetHelpText();
        /// <summary>
        /// The default value that will be used if no value was passed on the command line.
        /// </summary>
        public object DefaultValue { get; internal set; }
        /// <summary>
        /// Flag indicating whether this argument is required, i.e. must be provided via the command line.
        /// </summary>
        public bool IsRequired { get;  set; }

        public bool NeedsValue { get;  set; }

        public Argument(object defaultValue, bool isRequired = false)
        {
            IsRequired = isRequired;

            NeedsValue = true;
            Validator += Validate;

            if (IsRequired)
            {
                DefaultValue = defaultValue;
            }
        }

        public void Reset()
        {
            Value = DefaultValue;
        }

        /// <summary>A method that can be executed when the command line arguments are processed.</summary>
        public Action<object> Processor { get; set; }

        /// <summary>A method that can be used to validate a value for this argument.</summary>
        public Func<object, bool> Validator { get; set; }


        public virtual string GetHelpText() { return "HELP TEXT"; }

        /// <summary>Validates the specified value.</summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is valid; otherwise <c>false</c>.</returns>
        protected internal virtual bool Validate(object value)
        {
            return value != null || !IsRequired;
        }
        /// <summary>
        /// Process the value by executing the associated processor function.
        /// </summary>
        public void Process()
        {
            Processor?.Invoke(Value);
        }
    }

    public class Argument<T> : Argument
    {
        public Argument(T defaultValue, bool isRequired = false)
            : base(defaultValue, isRequired) { }

        public override string GetHelpText() => $"Argument vom Typ {typeof(T).Name} mit Standardwert: {DefaultValue}";


    }
}
