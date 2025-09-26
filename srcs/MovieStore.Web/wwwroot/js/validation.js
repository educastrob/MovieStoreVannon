document.addEventListener('DOMContentLoaded', function() {
    function validateEmail(email) {
        const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailRegex.test(email);
    }
    
    function validateCPF(cpf) {
        cpf = cpf.replace(/[^\d]/g, '');
        
        if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) return false;

        let sum = 0;
        for (let i = 0; i < 9; i++) {
            sum += parseInt(cpf[i]) * (10 - i);
        }
        let remainder = sum % 11;
        let firstDigit = remainder < 2 ? 0 : 11 - remainder;

        if (parseInt(cpf[9]) !== firstDigit) return false;

        sum = 0;
        for (let i = 0; i < 10; i++) {
            sum += parseInt(cpf[i]) * (11 - i);
        }
        remainder = sum % 11;
        let secondDigit = remainder < 2 ? 0 : 11 - remainder;

        return parseInt(cpf[10]) === secondDigit;
    }
    
    function validatePhone(phone) {
        phone = phone.replace(/[^\d]/g, '');
        
        if (phone.length !== 10 && phone.length !== 11) return false;
        
        const validDDDs = ['11','12','13','14','15','16','17','18','19','21','22','24','27','28','31','32','33','34','35','37','38','41','42','43','44','45','46','47','48','49','51','53','54','55','61','62','64','63','65','66','67','68','69','71','73','74','75','77','79','81','87','82','83','84','85','88','86','89','91','93','94','92','97','95','96','98','99'];
        const ddd = phone.substring(0, 2);
        
        return validDDDs.includes(ddd);
    }
    
    function formatCPF(value) {
        return value.replace(/\D/g, '')
                   .replace(/(\d{3})(\d)/, '$1.$2')
                   .replace(/(\d{3})(\d)/, '$1.$2')
                   .replace(/(\d{3})(\d{1,2})$/, '$1-$2');
    }
    
    function formatPhone(value) {
        const numbers = value.replace(/\D/g, '');
        if (numbers.length <= 10) {
            return numbers.replace(/(\d{2})(\d{4})(\d{0,4})/, '($1) $2-$3');
        } else {
            return numbers.replace(/(\d{2})(\d{5})(\d{0,4})/, '($1) $2-$3');
        }
    }
    
    document.querySelectorAll('[data-validation]').forEach(function(input) {
        input.addEventListener('input', function() {
            const validationType = this.getAttribute('data-validation');
            const value = this.value;
            const feedback = this.parentNode.querySelector('.invalid-feedback');
            let isValid = true;
            let message = '';
            
            if (validationType === 'cpf') {
                this.value = formatCPF(value);
            } else if (validationType === 'phone') {
                this.value = formatPhone(value);
            }
            
            if (value.trim() !== '') {
                switch (validationType) {
                    case 'email':
                        isValid = validateEmail(value);
                        message = isValid ? '' : 'Email inválido';
                        break;
                    case 'cpf':
                        isValid = validateCPF(value);
                        message = isValid ? '' : 'CPF inválido';
                        break;
                    case 'phone':
                        isValid = validatePhone(value);
                        message = isValid ? '' : 'Telefone inválido';
                        break;
                }
            }
            
            if (isValid) {
                this.classList.remove('is-invalid');
                this.classList.add('is-valid');
            } else {
                this.classList.remove('is-valid');
                this.classList.add('is-invalid');
            }

            if (feedback) {
                feedback.textContent = message;
            }
        });
        
        input.addEventListener('blur', function() {
            if (this.hasAttribute('required') && this.value.trim() === '') {
                this.classList.remove('is-valid', 'is-invalid');
            }
        });
    });
    
    document.querySelectorAll('form').forEach(function(form) {
        form.addEventListener('submit', function(e) {
            let hasErrors = false;
            
            this.querySelectorAll('[data-validation]').forEach(function(input) {
                if (input.classList.contains('is-invalid')) {
                    hasErrors = true;
                }
            });

            if (hasErrors) {
                e.preventDefault();
                alert('Por favor, corrija os erros antes de enviar o formulário.');
            }
        });
    });
});
