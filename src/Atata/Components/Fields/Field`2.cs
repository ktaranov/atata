﻿using System;

namespace Atata
{
    /// <summary>
    /// Represents the base class for the field controls. It can be used for HTML elements containing content (like &lt;h1&gt;, &lt;span&gt;, etc.) representing content as a field value, as well as for &lt;input&gt; and other elements.
    /// </summary>
    /// <typeparam name="T">The type of the control's data.</typeparam>
    /// <typeparam name="TOwner">The type of the owner page object.</typeparam>
    [ControlFinding(FindTermBy.Label)]
    public abstract class Field<T, TOwner> : Control<TOwner>, IEquatable<T>, IDataProvider<T, TOwner>
        where TOwner : PageObject<TOwner>
    {
        protected Field()
        {
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value => Get();

        /// <summary>
        /// Gets the value term options.
        /// </summary>
        protected TermOptions ValueTermOptions { get; private set; }

        UIComponent IDataProvider<T, TOwner>.Component => this;

        /// <summary>
        /// Gets the name of the data provider. The default value is "value".
        /// </summary>
        protected virtual string DataProviderName => "value";

        string IDataProvider<T, TOwner>.ProviderName => DataProviderName;

        TOwner IDataProvider<T, TOwner>.Owner => Owner;

        TermOptions IDataProvider<T, TOwner>.ValueTermOptions => ValueTermOptions;

        /// <summary>
        /// Gets the verification provider that gives a set of verification extension methods.
        /// </summary>
        public new FieldVerificationProvider<T, Field<T, TOwner>, TOwner> Should => new FieldVerificationProvider<T, Field<T, TOwner>, TOwner>(this);

        public static explicit operator T(Field<T, TOwner> field)
        {
            return field.Get();
        }

        public static bool operator ==(Field<T, TOwner> field, T value)
        {
            return field == null ? Equals(value, null) : field.Equals(value);
        }

        public static bool operator !=(Field<T, TOwner> field, T value)
        {
            return !(field == value);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>The value.</returns>
        protected abstract T GetValue();

        /// <summary>
        /// Gets the value. Also executes <see cref="TriggerEvents.BeforeGet"/> and <see cref="TriggerEvents.AfterGet"/> triggers.
        /// </summary>
        /// <returns>The value.</returns>
        public T Get()
        {
            ExecuteTriggers(TriggerEvents.BeforeGet);

            T value = GetValue();

            ExecuteTriggers(TriggerEvents.AfterGet);

            return value;
        }

        /// <summary>
        /// Converts the value to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value converted to string.</returns>
        protected internal virtual string ConvertValueToString(T value)
        {
            return (this as IDataProvider<T, TOwner>).ConvertValueToString(value);
        }

        /// <summary>
        /// Converts the string to value of <typeparamref name="T"/> type.
        /// </summary>
        /// <param name="value">The value as string.</param>
        /// <returns>The value converted to <typeparamref name="T"/> type.</returns>
        protected internal virtual T ConvertStringToValue(string value)
        {
            return TermResolver.FromString<T>(value, ValueTermOptions);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Field<T, TOwner> objAsField = obj as Field<T, TOwner>;
            if (objAsField != null)
            {
                return ReferenceEquals(this, objAsField);
            }
            else if (obj is T)
            {
                T objAsValue = (T)obj;
                return Equals(objAsValue);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(T other)
        {
            T value = Get();
            return Equals(value, other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected internal override void ApplyMetadata(UIComponentMetadata metadata)
        {
            base.ApplyMetadata(metadata);

            ValueTermOptions = new TermOptions();
            InitValueTermOptions(ValueTermOptions, metadata);
        }

        /// <summary>
        /// Initializes the value term options (culture, format, etc.).
        /// </summary>
        /// <param name="termOptions">The term options.</param>
        /// <param name="metadata">The component metadata.</param>
        protected virtual void InitValueTermOptions(TermOptions termOptions, UIComponentMetadata metadata)
        {
            termOptions.Culture = metadata.GetCulture();
            termOptions.Format = metadata.GetFormat();
        }
    }
}
