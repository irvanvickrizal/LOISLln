$(function() {

    Morris.Donut({
        element: 'morris-donut-chart',
        data: [{
            label: "MOS DONE",
            value: 12
        }, {
            label: "MOS Confirmation",
            value: 30
        }, {
            label: "As Plan BOQ Done",
            value: 20
        }],
        resize: true
    });

   
    
});
