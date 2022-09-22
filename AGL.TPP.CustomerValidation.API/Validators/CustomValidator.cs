using System;
using System.Globalization;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Helpers;
using AGL.TPP.CustomerValidation.API.Models;

namespace AGL.TPP.CustomerValidation.API.Validators
{
  /// <summary>
  /// Custom validator
  /// </summary>
  public static class CustomValidator
  {
    /// <summary>
    /// Regex that matches numbers with spaces
    /// </summary>
    public const string NumbersWithoutSpaces = @"^[0-9]*$";

    /// <summary>
    /// Validates the format of date
    /// </summary>
    /// <param name="dateValue">Input date value</param>
    /// <returns>Returns a boolean indicating whether date is valid or not</returns>
    public static bool CheckDateNotInFuture(string dateValue)
    {
      var currentMelbourneDate = DateTime.UtcNow.CurrentAestDateTime();
      var currentMelbourneDateEndOfDay = currentMelbourneDate.ToEndOfDay();

      DateTime.TryParseExact(dateValue, "yyyy-MM-dd",
          CultureInfo.InvariantCulture,
          DateTimeStyles.None,
          out var dateToCheck);

      var dateToCheckMelbourneDate = dateToCheck.ToUniversalTime().CurrentAestDateTime().ToEndOfDay();
      return dateToCheckMelbourneDate <= currentMelbourneDateEndOfDay;
    }

    public static bool CheckDateNotInThePast(string dateValue)
    {
      var currentMelbourneDate = DateTime.UtcNow.CurrentAestDateTime();
      var maximumPastDate = currentMelbourneDate.AddDays(-30).ToEndOfDay();

      DateTime.TryParseExact(dateValue, "yyyy-MM-dd",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out var dateToCheck);

      var dateToCheckMelbourneDate = dateToCheck.ToUniversalTime().CurrentAestDateTime().ToEndOfDay();
      return dateToCheckMelbourneDate >= maximumPastDate;
    }

    /// <summary>
    /// Validates the format of date
    /// </summary>
    /// <param name="dateValue">Input date value</param>
    /// <returns>Returns a boolean indicating whether date is valid or not</returns>
    public static bool CheckDateFormat(string dateValue)
    {
      if (DateTime.TryParseExact(dateValue, "yyyy-MM-dd",
          CultureInfo.InvariantCulture,
          DateTimeStyles.None,
          out _))
      {
        return true;
      }

      return false;
    }

    public static bool IsBoolean(string x)
    {
      return x.ToLowerInvariant() == "y" || x.ToLowerInvariant() == "n";
    }

    public static bool ValidateOfferTypes(string x)
    {
      var result = Enum.GetValues(typeof(OfferTypes))
              .OfType<OfferTypes>()
              .ToList()
              .Where(o => o != OfferTypes.Unknown)
              .Select(y => EnumHelper.GetDescription(y))
              .Any(e => e.ToLowerInvariant().Equals(x.ToLowerInvariant()));

      return result;
    }

    public static bool ValidateCancellationReasons(string x)
    {
      var result = Enum.GetValues(typeof(CancellationReasons))
              .OfType<CancellationReasons>()
              .ToList()
              .Where(o => o != CancellationReasons.Unknown)
              .Select(y => EnumHelper.GetDescription(y))
              .Any(e => e.ToLowerInvariant().Equals(x.ToLowerInvariant()));

      return result;
    }

    public static bool ValidateTransactionTypes(string x)
    {
      var result = Enum.GetValues(typeof(TransactionTypes))
              .OfType<TransactionTypes>()
              .ToList()
              .Where(o => o != TransactionTypes.Unknown)
              .Select(y => EnumHelper.GetDescription(y))
              .Any(e => e.ToLowerInvariant().Equals(x.ToLowerInvariant()));

      return result;
    }

    public static bool ValidateChannel(string x)
    {
      var result = Enum.GetValues(typeof(Channel))
              .OfType<Channel>()
              .ToList()
              .Where(o => o != Channel.Unknown)
              .Select(y => EnumHelper.GetDescription(y))
              .Any(e => e.ToLowerInvariant().Equals(x.ToLowerInvariant()));

      return result;
    }

    public static bool ValidateAddressPropertyUse(string x)
    {
      return x.Trim() == string.Empty || x.ToLowerInvariant() == Constants.AddressPropertyUseRented ||
             x.ToLowerInvariant() == Constants.AddressPropertyUseOwnerOccupied;
    }

    /// <summary>
    /// Validates the age as per business rules
    /// </summary>
    /// <param name="dateValue">Birth date</param>
    /// <returns>Returns a boolean indicating whether the age is valid or not</returns>
    public static bool ValidateAge(string dateValue)
    {
      DateTime.TryParse(dateValue,
          CultureInfo.InvariantCulture,
          DateTimeStyles.None,
          out var birthDate);

      DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd"),
          CultureInfo.InvariantCulture,
          DateTimeStyles.None,
          out DateTime today);

      var age = today.ToUniversalTime().Year - birthDate.ToUniversalTime().Year;

      if (birthDate.ToUniversalTime().Date > today.AddYears(-age)) age--;

      return age >= 16 && age < 110;
    }

    /// <summary>
    /// Validates whether at least one contact number is provided
    /// </summary>
    /// <param name="contact">Contact object</param>
    /// <returns>Returns a boolean indicating whether one contact detail is provided</returns>
    public static bool AtleastOneContact(Contact contact)
    {
      return contact.HomePhone?.Length > 0 || contact.MobilePhone?.Length > 0 || contact.WorkPhone?.Length > 0;
    }

    /// <summary>
    /// Validates whether at least one identification document is provided
    /// </summary>
    /// <param name="residentialIdentification">Residential identification document object</param>
    /// <returns>Returns a boolean indicating whether one identification document is provided</returns>

    public static bool AtleastOneIdentificationDocument(ResidentialIdentification residentialIdentification)
    {
      return residentialIdentification.DriversLicense?.LicenseNumber?.Length > 0 ||
                  residentialIdentification.Medicare?.MedicareNumber?.Length > 0 ||
                  residentialIdentification.Passport?.PassportNumber?.Length > 0;
    }

    public static bool IsResubmissionCountGreaterThanZero(string x)
    {
      int.TryParse(x, out int resubmissionCount);
      return resubmissionCount > 0;
    }
  }
}
