using System;
using AGL.TPP.CustomerValidation.API.Models;

namespace AGL.TPP.CustomerValidation.API.Helpers
{
    public static class FieldMapper
    {
        public static string MapPersonDetailTitle(string title)
        {
            if (title.Equals(Title.Unknown.ToString(), StringComparison.InvariantCultureIgnoreCase))
                return string.Empty;

            try
            {
                Enum.Parse<Title>(title, true);
                return title;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static ResidentialCustomerSalesValidationBodyModel GetMappedPayload(ResidentialCustomerSalesValidationBodyModel model)
        {
            if (model.PersonDetail == null) return model;

            model.PersonDetail.Title = MapPersonDetailTitle(model.PersonDetail.Title);

            return model;
        }

        public static CancellationResidentialCustomerValidationBodyModel GetMappedPayload(CancellationResidentialCustomerValidationBodyModel model)
        {
            if (model.PersonDetail == null) return model;

            model.PersonDetail.Title = MapPersonDetailTitle(model.PersonDetail.Title);

            return model;
        }

        public static ResidentialCustomerMoveOutValidationBodyModel GetMappedPayload(ResidentialCustomerMoveOutValidationBodyModel model)
        {
            if (model.PersonDetail == null) return model;

            model.PersonDetail.Title = MapPersonDetailTitle(model.PersonDetail.Title);

            return model;
        }

        public static ResidentialCustomerChangeValidationBodyModel GetMappedPayload(ResidentialCustomerChangeValidationBodyModel model)
        {
            if (model.PersonDetail == null) return model;

            model.PersonDetail.Title = MapPersonDetailTitle(model.PersonDetail.Title);

            return model;
        }
    }
}
