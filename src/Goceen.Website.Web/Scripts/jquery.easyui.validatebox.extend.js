$.extend($.fn.validatebox.defaults.rules, {    
    eq: {
        validator: function (value, param) {
            $.fn.validatebox.defaults.rules.eq.message = param[1];
            return value == $(param[0]).val();
        },
        message: ''
    },
    range: {
        validator: function (value, param) {
            $.fn.validatebox.defaults.rules.range.message = param[2];
            return value >= param[0] && value <= param[1];
        },
        message: ''
    },
    regular: {
        validator: function (value, param) {
            $.fn.validatebox.defaults.rules.regular.message = param[1];
            var reg = new RegExp(param[0]);
            return reg.test(value);
        },
        message: ''
    },
    number: {
        validator: function (value, param) {
            //$.fn.validatebox.defaults.rules.number.message = param;
            return /^\d+$/.test(value);
        },
        message: '请输入数字'
    }
});
