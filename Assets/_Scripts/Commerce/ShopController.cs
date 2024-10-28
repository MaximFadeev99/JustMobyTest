using JustMobyTest.Commerce.View;
using JustMobyTest.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JustMobyTest.Commerce
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Button _enterShopButton;
        [SerializeField] private Button _exitShopButton;
        [SerializeField] private Button _increaseAmountButton;
        [SerializeField] private Button _decreaseAmountButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Button _purchaseWithDiscountButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private ToggleGroup _purchaseOptions;

        private SignalBus _signalBus;

        public void Initialize(SignalBus signalBus) 
        {
            _signalBus = signalBus;

            _enterShopButton.onClick.AddListener(OnEnterShopPressed);
            _exitShopButton.onClick.AddListener(OnExitShopPressed);
            _increaseAmountButton.onClick.AddListener(OnIncreaseButtonPressed);
            _decreaseAmountButton.onClick.AddListener(OnDecreaseButtonPressed);
            _continueButton.onClick.AddListener(OnContinueButtonPressed);
            _returnButton.onClick.AddListener(OnReturnButtonPressed);
            _purchaseButton.onClick.AddListener(OnPurchaseButtonPressed);
            _purchaseWithDiscountButton.onClick.AddListener(OnPurchaseButtonPressed);
        }

        public void Dispose() 
        {
            _enterShopButton.onClick.RemoveListener(OnEnterShopPressed);
            _exitShopButton.onClick.RemoveListener(OnExitShopPressed);
            _increaseAmountButton.onClick.RemoveListener(OnIncreaseButtonPressed);
            _decreaseAmountButton.onClick.RemoveListener(OnDecreaseButtonPressed);
            _continueButton.onClick.RemoveListener(OnContinueButtonPressed);
            _returnButton.onClick.RemoveListener(OnReturnButtonPressed);
            _purchaseButton.onClick.RemoveListener(OnPurchaseButtonPressed);
            _purchaseWithDiscountButton.onClick.RemoveListener(OnPurchaseButtonPressed);
        }

        #region Callback Methods
        private void OnEnterShopPressed() 
        {
            _enterShopButton.gameObject.SetActive(false);
            _signalBus.Fire(new EnteringShopSignal());
        }

        private void OnExitShopPressed() 
        {
            _enterShopButton.gameObject.SetActive(true);
            _signalBus.Fire(new ExitingShopSignal());
        }

        private void OnIncreaseButtonPressed() 
        {
            _signalBus.Fire(new IncreaseCountSignal());
        }

        private void OnDecreaseButtonPressed()
        {
            _signalBus.Fire(new DecreaseCountSignal());
        }

        private void OnContinueButtonPressed() 
        {
            Toggle activeToggle = _purchaseOptions.GetFirstActiveToggle();

            if (activeToggle == null)
                return;
            
            _signalBus.Fire(new ContinueSignal(activeToggle.GetComponent<AssortmentIcon>().BundleName));
        }

        private void OnReturnButtonPressed() 
        {
            _signalBus.Fire(new ReturnSignal());
        }

        private void OnPurchaseButtonPressed() 
        {
            _signalBus.Fire(new PurchaseSignal());
        }
        #endregion
    }
}
