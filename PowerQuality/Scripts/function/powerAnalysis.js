var powerDatas = {
    'activeValues': [],
    'harmonics': []
};

var powerAnalysis = {
    'fetch': function (url, startIndex, totalRequest, target) {
        if (totalRequest <= 0) return;
        var requestCount = totalRequest > 3600 ? 3600 : totalRequest;
        base.AjaxGet(url, { StartIndex: startIndex, RequestCount: requestCount }, null, function (ret) {
            ret.recordData.forEach(function (data) {
                target.push(data);
            });
            powerAnalysis.fetch(url, startIndex + requestCount, totalRequest - 3600, target);
        });
    }
};

$(function () {
    powerAnalysis.fetch('/PowerAnalysis/RecordData', 0, 14400, powerDatas.activeValues);
    powerAnalysis.fetch('/PowerAnalysis/RecordHarmonic', 0, 14400, powerDatas.harmonics);
});