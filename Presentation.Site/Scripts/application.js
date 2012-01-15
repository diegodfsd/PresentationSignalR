(function ($) {
    $.fn.extend({
        simpletabs: function () {
            var items = []
              , links = $("a", this);

            $.each(links, function (i) {
                items.push({
                    anchor: $(this),
                    view: $(this).attr("href"),
                    show: function () {
                        $(this.view).show();
                    },
                    hide: function () {
                        $(this.view).hide();
                    }
                });
            });


            var view = function () { };
            view.collapse = function () {
                $.each(items, function () {
                    this.hide();
                });
            };

            view.handler = function () {
                var self = this;
                $.each(items, function (i) {
                    $(this.anchor).click(function () {
                        self.collapse();
                        (function (index) {
                            items[index].show();
                        })(i);
                    });
                });
            };

            view.init = function () {
                this.handler();
                this.collapse();
                items[0].show();
            };

            view.init();
        }
    });
})(jQuery);