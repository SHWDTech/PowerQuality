var loadPercentage = 0;

var powerDatas = {
    'activeValues': {
        'pusuRange': function (propName, propValues) {
            if (IsNullOrEmpty(powerDatas.activeValues[propName])) {
                powerDatas.activeValues[propName] = [];
            }
            propValues.forEach(function (value) {
                powerDatas.activeValues[propName].push(value);
            });
        }
    },
    'harmonics': {
        'pusuRange': function (propName, propValues) {
            if (IsNullOrEmpty(powerDatas.harmonics[propName])) {
                powerDatas.harmonics[propName] = [];
            }

            propValues.forEach(function (value) {
                powerDatas.harmonics[propName].push(value);
            });
        }
    }
};

var powerAnalysis = {
    'selectChannel': [],
    'fetch': function (url, startIndex, totalRequest, target) {
        if (totalRequest <= 0) {
            loadPercentage += 50;
            powerAnalysis.loadComplete();
            return;
        }
        var requestCount = totalRequest > 3600 ? 3600 : totalRequest;
        base.AjaxGet(url, { StartIndex: startIndex, RequestCount: requestCount }, null, function (ret) {
            $.each(ret.recordData, function (key, value) {
                target.pusuRange(key, value);
            });
            powerAnalysis.fetch(url, startIndex + requestCount, totalRequest - 3600, target);
        });
    },
    'loadComplete': function () {
        $('#percentage').html(loadPercentage);
        if (loadPercentage === 100) {
            $('#pageLoading').hide('slow');
            $('#mainContent').show('slow');
            powerAnalysis.initCharts();
        }
    },
    'initCharts': function () {
        $('#chartContainer').empty();
        var maxHeight = 800;
        var chartHeights = maxHeight / powerAnalysis.selectChannel.length;
        powerAnalysis.selectChannel.forEach(function (channel) {
            var chart = document.createElement('div');
            $(chart).attr('height', chartHeights);
            $(chart).attr('id', channel + '_chart');
            $('#chartContainer').append(chart);
            echarts.initCharts(chart);
        });
    },
    'setChannels': function (selects) {
        powerAnalysis.selectChannel = [];
        $.each(selects, function (key, value) {
            if ($(value).attr('checked')) {
                powerAnalysis.selectChannel.push($(value).attr('id'));
            }
        });
    },
    'setupConfig': function () {
        powerAnalysis.setChannels($('#channelOptions input[type="checkbox"]'));
    }
};

$(function () {
    powerAnalysis.setupConfig();
    powerAnalysis.fetch('/PowerAnalysis/RecordData', 0, 14400, powerDatas.activeValues, powerAnalysis.loadComplete());
    powerAnalysis.fetch('/PowerAnalysis/RecordHarmonic', 0, 14400, powerDatas.harmonics, powerAnalysis.loadComplete());
});