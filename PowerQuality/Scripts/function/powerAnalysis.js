var getPercentate = function() {
    base.AjaxGet('/PowerAnalysis/LoadPercentage', { recordGuid: $('#recordId').attr('recordid') }, function (ret) {
        $('#percentage').html(Math.round(ret.percentage * 10000) / 100);
        if (ret.percentage >= 1) return;

        setTimeout(function() { getPercentate(); }, 500);
    });
}

$(function () {
    getPercentate();
});