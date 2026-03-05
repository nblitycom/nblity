/* Auth form submission helper for Blazor components */
window.nblityAuth = {
    submitForm: function (url, data) {
        var form = document.createElement('form');
        form.method = 'POST';
        form.action = url;

        for (var key in data) {
            if (data.hasOwnProperty(key)) {
                var input = document.createElement('input');
                input.type = 'hidden';
                input.name = key;
                input.value = data[key];
                form.appendChild(input);
            }
        }

        document.body.appendChild(form);
        form.submit();
    }
};
