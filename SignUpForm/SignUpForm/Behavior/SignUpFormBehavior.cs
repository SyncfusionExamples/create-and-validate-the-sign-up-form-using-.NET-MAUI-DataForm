namespace SignUpForm
{
    using Syncfusion.Maui.DataForm;
    using System;

    public class SignUpFormBehavior : Behavior<ContentPage>
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        ///  Holds the sign up button instance.
        /// </summary>
        private Button? signUpButton;

        /// <summary>
        ///  Holds the cancel button instance.
        /// </summary>
        private Button? cancelButton;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.FindByName<SfDataForm>("signUpForm");
            if (dataForm != null)
            {
                dataForm.DefaultLayoutSettings.LabelPosition = DataFormLabelPosition.Top;
                dataForm.ColumnCount = 2;
                dataForm.ItemsSourceProvider = new ItemsSourceProvider();
                dataForm.RegisterEditor("Country", DataFormEditorType.AutoComplete);
                dataForm.RegisterEditor("MobileNumber", new NumericEditor(dataForm));
                dataForm.RegisterEditor("ZipCode", new NumericEditor(dataForm));
                this.dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;
            }

            this.cancelButton = bindable.FindByName<Button>("cancelButton");
            this.signUpButton = bindable.FindByName<Button>("signUpButton");

            if (this.cancelButton != null)
            {
                this.cancelButton.Clicked += OnCancelButtonClicked;
            }

            if (this.signUpButton != null)
            {
                this.signUpButton.Clicked += OnSignUpButtonClicked;
            }
        }

        /// <summary>
        /// Invokes on data form item generation.
        /// </summary>
        /// <param name="sender">Te data form</param>
        /// <param name="e">The event arguments.</param>
        private void OnGenerateDataFormItem(object? sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null)
            {
#if WINDOWS || MACCATALYST
                if (e.DataFormItem.FieldName == nameof(SignUpFormModel.FirstName) || e.DataFormItem.FieldName == nameof(SignUpFormModel.LastName)
                    || e.DataFormItem.FieldName == nameof(SignUpFormModel.Email) || e.DataFormItem.FieldName == nameof(SignUpFormModel.MobileNumber)
                    || e.DataFormItem.FieldName == nameof(SignUpFormModel.Password) || e.DataFormItem.FieldName == nameof(SignUpFormModel.RetypePassword))
                {
                    e.DataFormItem.ColumnSpan = 1;
                }
#endif
                if (e.DataFormItem.FieldName == nameof(SignUpFormModel.Country) && e.DataFormItem is DataFormAutoCompleteItem autoComplete)
                {
                    autoComplete.MaxDropDownHeight = 200;
                }
                else if (e.DataFormItem.FieldName == nameof(SignUpFormModel.Email) && e.DataFormItem is DataFormTextEditorItem textItem)
                {
                    textItem.Keyboard = Keyboard.Email;
                }
            }
        }

        /// <summary>
        /// Invokes on sign up button click.
        /// </summary>
        /// <param name="sender">The sign up button.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnSignUpButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    await App.Current.MainPage.DisplayAlert("", "Signed up successfully", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        /// <summary>
        /// Invokes on cancel button click.
        /// </summary>
        /// <param name="sender">The cancel button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnCancelButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null)

            {
                this.dataForm.DataObject = new SignUpFormModel();
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.cancelButton != null)
            {
                this.cancelButton.Clicked -= OnCancelButtonClicked;
            }

            if (this.signUpButton != null)
            {
                this.signUpButton.Clicked -= OnSignUpButtonClicked;
            }

            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
            }
        }
    }
}
