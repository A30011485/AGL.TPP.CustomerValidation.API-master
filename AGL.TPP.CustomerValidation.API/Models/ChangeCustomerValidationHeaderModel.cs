using AGL.TPP.CustomerValidation.API.Helpers;
using Destructurama.Attributed;
using System;

namespace AGL.TPP.CustomerValidation.API.Models
{
  /// <summary>
  ///
  /// </summary>
  public class ChangeCustomerValidationHeaderModel
  {
    /// <summary>
    /// Gets or Sets VendorBusinessPartnerNumber
    /// </summary>
    public string VendorBusinessPartnerNumber { get; set; }

    /// <summary>
    /// Gets or Sets VendorName
    /// </summary>
    public string VendorName { get; set; }

    /// <summary>
    /// Gets or Sets Channel
    /// </summary>
    private string _channel;

    /// <summary>
    /// Sale originating channel - Refer <cref name="Channel"></cref> for more details
    /// </summary>
    public string Channel
    {
      get
      {
        Enum.TryParse(_channel, true, out Channel c);
        return EnumHelper.GetDescription(c);
      }
      set
      {
        this._channel = value;
      }
    }

    /// <summary>
    /// Gets or Sets Retailer
    /// </summary>
    public string Retailer { get; set; }

    /// <summary>
    /// Type of transaction - Refer <cref name="TransactionTypes"></cref> for more details
    /// </summary>
    public string TransactionType { get; set; }

    /// <summary>
    /// Gets or Sets VendorLeadId
    /// </summary>
    public string VendorLeadId { get; set; }

    /// <summary>
    /// Resubmission count
    /// </summary>
    public string ResubmissionCount { get; set; }

    /// <summary>
    /// Resubmission comment
    /// </summary>
    [LogMasked]
    public string ResubmissionComments { get; set; }

    private string _offerType;

    public string OfferType
    {
      get
      {
        Enum.TryParse(_offerType, true, out OfferTypes ot);
        return EnumHelper.GetDescription(ot);
      }
      set
      {
        _offerType = value;
      }
    }
  }
}
