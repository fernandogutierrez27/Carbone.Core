const carbone = require('carbone');

module.exports = {
    createXlsx: function (callback, dataIn, nombreReporte) {
        var data = JSON.parse(dataIn);


        carbone.render(`./Templates/${nombreReporte}.xlsx`, data, function (err, result) {
            if (err) {
                callback(err, null);
            }
           
            callback(null, result);
        })
    },

    createPdf: function (callback, dataIn, nombreReporte) {
        var data = JSON.parse(dataIn);
        var options = {
            convertTo: 'pdf' //can be docx, txt, ...
        };


        carbone.render(`./Templates/${nombreReporte}.docx`, data, options, function (err, result) {
            if (err) {
                callback(err, null);
            }

            callback(null, result);
        })
    },

    createDocx: function (callback, dataIn, nombreReporte) {
        var data = JSON.parse(dataIn);
        //var options = {
        //    convertTo: 'pdf' //can be docx, txt, ...
        //};
        carbone.render(`./Templates/${nombreReporte}.docx`, data, function (err, result) {
            if (err) {
                callback(err, null);
            }

            callback(null, result);
        })
    }
};