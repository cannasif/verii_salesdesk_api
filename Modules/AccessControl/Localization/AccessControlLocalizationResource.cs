using salesdesk_api.Shared.Common.Application.Common;

namespace salesdesk_api.Modules.AccessControl.Localization;

public sealed class AccessControlLocalizationResource : ILocalizationResource
{
    public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> MessagesByCulture { get; } =
        new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["en"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["VisibilityPolicyService.NotFound"] = "Visibility policy not found.",
                ["VisibilityPolicyService.CodeAlreadyExists"] = "Visibility policy code already exists.",
                ["VisibilityPolicyService.InvalidScopeType"] = "Invalid visibility scope type.",
                ["VisibilityPolicyService.InvalidEntityType"] = "Invalid visibility entity type.",
                ["VisibilityPolicyService.Retrieved"] = "Visibility policy retrieved.",
                ["VisibilityPolicyService.Created"] = "Visibility policy created.",
                ["VisibilityPolicyService.Updated"] = "Visibility policy updated.",
                ["VisibilityPolicyService.Deleted"] = "Visibility policy deleted.",
                ["VisibilityPolicyService.ListRetrieved"] = "Visibility policies retrieved.",
                ["UserVisibilityPolicyService.NotFound"] = "User visibility policy not found.",
                ["UserVisibilityPolicyService.UserAlreadyHasPolicy"] = "This user already has the selected visibility policy.",
                ["UserVisibilityPolicyService.Retrieved"] = "User visibility policy retrieved.",
                ["UserVisibilityPolicyService.Created"] = "User visibility policy created.",
                ["UserVisibilityPolicyService.Updated"] = "User visibility policy updated.",
                ["UserVisibilityPolicyService.Deleted"] = "User visibility policy deleted.",
                ["UserVisibilityPolicyService.ListRetrieved"] = "User visibility policies retrieved.",
                ["UserVisibilityPolicyService.VisibilityPolicyNotFound"] = "Visibility policy not found.",
                ["VisibilityPolicyController.UserIdEntityTypeRequired"] = "userId and entityType are required.",
                ["VisibilityPolicyController.PreviewValidationFailed"] = "Visibility preview validation failed.",
                ["VisibilityPolicyController.PreviewRetrieved"] = "Visibility preview retrieved.",
                ["VisibilityPolicyController.SimulationParamsRequired"] = "userId, entityType, and entityId are required.",
                ["VisibilityPolicyController.SimulationValidationFailed"] = "Visibility simulation validation failed.",
                ["VisibilityPolicyController.SimulationRetrieved"] = "Visibility action simulation retrieved.",
            },
            ["tr"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["VisibilityPolicyService.NotFound"] = "G\u00f6r\u00fcn\u00fcrl\u00fck politikasi bulunamad\u0131.",
                ["VisibilityPolicyService.CodeAlreadyExists"] = "G\u00f6r\u00fcn\u00fcrl\u00fck politikasi kodu zaten mevcut.",
                ["VisibilityPolicyService.InvalidScopeType"] = "Ge\u00e7ersiz g\u00f6r\u00fcn\u00fcrl\u00fck kapsam\u0131 tipi.",
                ["VisibilityPolicyService.InvalidEntityType"] = "Ge\u00e7ersiz g\u00f6r\u00fcn\u00fcrl\u00fck kay\u0131t tipi.",
                ["VisibilityPolicyService.Retrieved"] = "G\u00f6r\u00fcn\u00fcrl\u00fck politikasi getirildi.",
                ["VisibilityPolicyService.Created"] = "G\u00f6r\u00fcn\u00fcrl\u00fck politikasi olu\u015fturuldu.",
                ["VisibilityPolicyService.Updated"] = "G\u00f6r\u00fcn\u00fcrl\u00fck politikasi g\u00fcncellendi.",
                ["VisibilityPolicyService.Deleted"] = "G\u00f6r\u00fcn\u00fcrl\u00fck politikasi silindi.",
                ["VisibilityPolicyService.ListRetrieved"] = "G\u00f6r\u00fcn\u00fcrl\u00fck politikalar\u0131 getirildi.",
                ["UserVisibilityPolicyService.NotFound"] = "Kullan\u0131c\u0131 g\u00f6r\u00fcn\u00fcrl\u00fck politikas\u0131 bulunamad\u0131.",
                ["UserVisibilityPolicyService.UserAlreadyHasPolicy"] = "Bu kullan\u0131c\u0131 se\u00e7ilen g\u00f6r\u00fcn\u00fcrl\u00fck politikas\u0131na zaten sahip.",
                ["UserVisibilityPolicyService.Retrieved"] = "Kullan\u0131c\u0131 g\u00f6r\u00fcn\u00fcrl\u00fck politikas\u0131 getirildi.",
                ["UserVisibilityPolicyService.Created"] = "Kullan\u0131c\u0131 g\u00f6r\u00fcn\u00fcrl\u00fck politikas\u0131 olu\u015fturuldu.",
                ["UserVisibilityPolicyService.Updated"] = "Kullan\u0131c\u0131 g\u00f6r\u00fcn\u00fcrl\u00fck politikas\u0131 g\u00fcncellendi.",
                ["UserVisibilityPolicyService.Deleted"] = "Kullan\u0131c\u0131 g\u00f6r\u00fcn\u00fcrl\u00fck politikas\u0131 silindi.",
                ["UserVisibilityPolicyService.ListRetrieved"] = "Kullan\u0131c\u0131 g\u00f6r\u00fcn\u00fcrl\u00fck politikalar\u0131 getirildi.",
                ["UserVisibilityPolicyService.VisibilityPolicyNotFound"] = "G\u00f6r\u00fcn\u00fcrl\u00fck politikasi bulunamad\u0131.",
                ["VisibilityPolicyController.UserIdEntityTypeRequired"] = "userId ve entityType zorunludur.",
                ["VisibilityPolicyController.PreviewValidationFailed"] = "\u00d6nizleme do\u011frulamas\u0131 ba\u015far\u0131s\u0131z.",
                ["VisibilityPolicyController.PreviewRetrieved"] = "G\u00f6r\u00fcn\u00fcrl\u00fck \u00f6nizlemesi getirildi.",
                ["VisibilityPolicyController.SimulationParamsRequired"] = "userId, entityType ve entityId zorunludur.",
                ["VisibilityPolicyController.SimulationValidationFailed"] = "Sim\u00fclasyon do\u011frulamas\u0131 ba\u015far\u0131s\u0131z.",
                ["VisibilityPolicyController.SimulationRetrieved"] = "G\u00f6r\u00fcn\u00fcrl\u00fck eylem sim\u00fclasyonu getirildi.",
            },
        };
}
