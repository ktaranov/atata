﻿namespace Atata
{
    public static class EditableFieldExtensions
    {
        /// <summary>
        /// Sets the random value and records it to <paramref name="value"/> parameter.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner page object.</typeparam>
        /// <param name="field">The editable field control.</param>
        /// <param name="value">The generated value.</param>
        /// <returns>The instance of the owner page object.</returns>
        public static TOwner SetRandom<TOwner>(this EditableField<decimal?, TOwner> field, out int? value)
            where TOwner : PageObject<TOwner>
        {
            decimal? decimalValue;
            field.SetRandom(out decimalValue);

            value = (int?)decimalValue;

            return field.Owner;
        }

        /// <summary>
        /// Sets the random value and records it to <paramref name="value"/> parameter.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner page object.</typeparam>
        /// <param name="field">The editable field control.</param>
        /// <param name="value">The generated value.</param>
        /// <returns>The instance of the owner page object.</returns>
        public static TOwner SetRandom<TOwner>(this EditableField<decimal?, TOwner> field, out int value)
            where TOwner : PageObject<TOwner>
        {
            decimal? decimalValue;
            field.SetRandom(out decimalValue);

            value = (int)decimalValue;

            return field.Owner;
        }

        /// <summary>
        /// Sets the random value and records it to <paramref name="value"/> parameter.
        /// </summary>
        /// <typeparam name="TData">The type of the control's data.</typeparam>
        /// <typeparam name="TOwner">The type of the owner page object.</typeparam>
        /// <param name="field">The editable field control.</param>
        /// <param name="value">The generated value.</param>
        /// <returns>The instance of the owner page object.</returns>
        public static TOwner SetRandom<TData, TOwner>(this EditableField<TData?, TOwner> field, out TData value)
            where TData : struct
            where TOwner : PageObject<TOwner>
        {
            TData? nullableValue;
            field.SetRandom(out nullableValue);

            value = (TData)nullableValue;

            return field.Owner;
        }
    }
}
