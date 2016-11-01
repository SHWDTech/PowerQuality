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
    },
    'voltageCurrentSecond': {
        'pusuRange': function (propName, propValues) {
            if (IsNullOrEmpty(powerDatas.voltageCurrentSecond[propName])) {
                powerDatas.voltageCurrentSecond[propName] = [];
            }

            propValues.forEach(function (value) {
                powerDatas.voltageCurrentSecond[propName].push(value);
            });
        }
    },
    'voltageCurrentThreeSecond': {
        'pusuRange': function (propName, propValues) {
            if (IsNullOrEmpty(powerDatas.voltageCurrentThreeSecond[propName])) {
                powerDatas.voltageCurrentThreeSecond[propName] = [];
            }

            propValues.forEach(function (value) {
                powerDatas.voltageCurrentThreeSecond[propName].push(value);
            });
        }
    }
};

var powerAnalysis = {
    'selectChannel': [],
    'fetch': function (url, startIndex, totalRequest, target) {
        if (totalRequest <= 0) {
            loadPercentage += 25;
            powerAnalysis.loadComplete();
            return;
        }
        var requestCount = totalRequest > 3600 ? 3600 : totalRequest;
        base.AjaxGet(url, { StartIndex: startIndex, RequestCount: requestCount, RecordGuid: $('#recordId').attr('recordId') }, null, function (ret) {
            $.each(ret.recordData, function (key, value) {
                target.pusuRange(key, value);
            });
            powerAnalysis.fetch(url, startIndex + requestCount, totalRequest - 3600, target);
        });
    },
    'loadComplete': function () {
        $('#percentage').html(loadPercentage);
        if (loadPercentage === 100) {
            powerAnalysis.initCharts();
            $('#pageLoading').hide('slow');
            $('#mainContent').show('slow');
        }
    },
    'initCharts': function () {
        $('#chartContainer').empty();
        var maxHeight = 800;
        var chartHeights = maxHeight / powerAnalysis.selectChannel.length;
        powerAnalysis.selectChannel.forEach(function (channel, index) {
            var chart = document.createElement('div');
            $(chart).attr('id', channel + '_chart');
            $('#chartContainer').append(chart);
            $(chart).height(chartHeights);
            $(chart).width($('#pageLoading').outerWidth(true) - 50);
            var current = echarts.init(chart);
            if (index !== 0) {
                $(chart).css('margin-top', '-25px');
            }
            powerCharts.push(current);
            current.setOption(powerAnalysis.getChartOption(channel, index));
            echarts.connect(powerCharts);
        });
    },
    'getChartOption': function (channel, index) {
        var max = powerDatas.voltageCurrentThreeSecond[channel + '_Max'];
        var avg = powerDatas.voltageCurrentThreeSecond[channel + '_Avg'];
        var min = powerDatas.voltageCurrentThreeSecond[channel + '_Min'];
        var maxVal = Math.round(Math.max.apply(null, max.concat(avg).concat(min)) * 100) / 100;
        var minVal = Math.round(Math.min.apply(null, max.concat(avg).concat(min)) * 100) / 100;
        var interval = Math.round((maxVal - minVal) / 4 * 100) / 100;
        var par = {
            'category': powerDatas.voltageCurrentThreeSecond['RecordTime'].map(function (date) {
                return new Date(date);
            }),
            'max': max,
            'avg': avg,
            'min': min,
            'maxVal': Math.round((maxVal + interval) * 100) / 100,
            'minVal': Math.round((minVal - interval) * 100) / 100,
            'interval': interval,
            'axisLabel': true,
            'index': index
        }

        if (index < powerAnalysis.selectChannel.length - 1) {
            par['axisLabel'] = false;
        }

        return chartsOption.StepLine(par);
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
    powerAnalysis.fetch('/PowerAnalysis/VoltageCurrentSecond', 0, 3600, powerDatas.voltageCurrentSecond, powerAnalysis.loadComplete());
    powerAnalysis.fetch('/PowerAnalysis/VoltageCurrentThreeSecond', 0, 1200, powerDatas.voltageCurrentThreeSecond, powerAnalysis.loadComplete());
});