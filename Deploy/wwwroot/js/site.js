$(document).ready(function () {

    $('.table').tablesorter({
        sortList: [[1, 0]]
    });



    var ua = navigator.userAgent.toLowerCase();
    var ios_devices = ua.match(/(ipad|iphone)/) ? "touchstart" : "click";

    $('.navbtn').bind(ios_devices, function () {
        $('#responsive').fadeIn();
    });
    var num = $('#search').val();
    $('#search').focus().val('').val(num);

    var searchValue = $('#search').val();

    $(function () {
        setTimeout(checkSearchChanged, 0.1);
    });

    function checkSearchChanged() {
        var currentValue = $('#search').val();
        if ((currentValue) && currentValue != searchValue && currentValue != '') {
            searchValue = $('#search').val();
            $('#submit').click();

        }
        else {
            setTimeout(checkSearchChanged, 0.1);
        }
    }

    
    //$('#deploynames').change(function () {
    //    var selected = $('#deploynames :selected');
    //    var group = selected.parent().attr('label');
    //    if (group == "Virtual Machines" || group == "Special")
    //    {
    //        $('#options').slideDown(1000);
    //    }
    //    else
    //    {
    //        $('#options').slideUp(1000);

    //    }

    //    if (selected.val() == "RDS" || selected.val() == "RDSTypeTec") {
    //        $('#datadisk').removeClass('disabled').fadeIn(1000).css({ 'margin-left': '15px' });
    //        $('#domain').addClass('disabled').fadeOut(1000);
    //    }
    //    else if (selected.val() == "Identity") {
    //         $('#test, #datadisk').addClass('disabled').fadeOut(1000);
    //         $('#domain').removeClass('disabled').fadeIn(1000).css({ 'margin-left': '15px' });
    //    }
    //    else if (selected.val() == "FileSrvTypeTec") {
    //        $('#datadisk, #domain').addClass('disabled').fadeOut(1000);
    //    }
    //    else {
    //         $('#domain, #datadisk').removeClass('disabled').fadeIn(1000);
    //         $('#domain').css({ 'margin-left': '15px' });
    //    }
    //});

    //var oldoptions = [];

    //$('#domain').on('click', function () {
    //    $('#deploynames').append(oldoptions);
    //    oldoptions = $("#deploynames option:not(:contains(" + $(this).val() + "))").detach();
    //});

    $('.button-checkbox').each(function () {

        // Settings
        var $widget = $(this),
            $button = $widget.find('button'),
            $checkbox = $widget.find('input:checkbox'),
            color = $button.data('color'),
            settings = {
                on: {
                    icon: 'glyphicon glyphicon-check'
                },
                off: {
                    icon: 'glyphicon glyphicon-unchecked'
                }
            };

        // Event Handlers
        $button.on('click', function () {
            $checkbox.prop('checked', !$checkbox.is(':checked'));
            $checkbox.triggerHandler('change');
            updateDisplay();
        });
        $checkbox.on('change', function () {
            updateDisplay();
        });

        // Actions
        function updateDisplay() {
            var isChecked = $checkbox.is(':checked');

            // Set the button's state
            $button.data('state', (isChecked) ? "on" : "off");

            // Set the button's icon
            $button.find('.state-icon')
                .removeClass()
                .addClass('state-icon ' + settings[$button.data('state')].icon);

            // Update the button's color
            if (isChecked) {
                $button
                    .removeClass('btn-default')
                    .addClass('btn-' + color + ' active');
            }
            else {
                $button
                    .removeClass('btn-' + color + ' active')
                    .addClass('btn-default');
            }
        }

        // Initialization
        function init() {

            updateDisplay();

            // Inject the icon if applicable
            if ($button.find('.state-icon').length == 0) {
                $button.prepend('<i class="state-icon ' + settings[$button.data('state')].icon + '"></i> ');
            }
        }
        init();
    });











 });



