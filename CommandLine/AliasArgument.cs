using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsocasm.CommandLine
{
    /// <summary>Argument that acts as an alias for another argument.</summary>
    public class AliasArgument : Argument
    {
        private readonly Argument _reference;

        /// <summary>Initializes a new instance of the <see cref="AliasArgument"/> class.</summary>
        /// <param name="entry">The argument this alias should mirror.</param>
        public AliasArgument(Argument entry)
            : base(entry.DefaultValue, entry.IsRequired)
        {
            _reference = entry;
        }

        /// <summary>Gets the value of this argument.</summary>
        /// <returns>The argument's value.</returns>
        public override T GetValue<T>()
        {
            return _reference.GetValue<T>();
        }

        /// <summary>Sets the value for this argument.</summary>
        /// <param name="value">The value to set.</param>
        public override void SetValue<T>(T value)
        {
                _reference.SetValue(value);
        }

        /// <summary>
        /// The default value that will be used if no value was passed on the command line.
        /// </summary>
        /// <remarks>Using this when <see cref="IsRequired"/> is set will have no effect.</remarks>
        public new object DefaultValue
        {
            get { return _reference.DefaultValue; }

            protected internal set { _reference.DefaultValue = value; }
        }

        /// <summary>
        /// Flag indicating whether this argument is required, i.e. must be provided via the command line.
        /// </summary>
        public new bool IsRequired => _reference.IsRequired;


        /// <summary>Validates the specified value.</summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is valid; otherwise <c>false</c>.</returns>
        protected internal override bool Validate(object value)
        {
            return _reference.Validate(value);
        }
    }
}
