(function ($) {
    $.fn.extend({
        tabs: function () {
            var items;
            items = $("a", this);

            var collapse = function () {
                items.each(function () {
                    toggle($(this), false);
                });
            };

            var toggle = function (anchor, show) {
                $(anchor.attr('href'))[show ? "show" : "hide"]();
            };

            var handle = function () {
                items.each(function () {
                    $(this).click(function () {
                        collapse();
                        toggle($(this), true);
                    });
                });
            };

            var init = function () {
                handle();
                collapse();
                $(items[0]).trigger('click');
            }

            init();
        }
    });
})(jQuery);