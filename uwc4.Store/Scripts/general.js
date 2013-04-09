$(function () {
	$(".tab-content:first").show();
	$(".nav-tabs li").click(function() {
		var t = $(this);
		if (!t.hasClass("active")) {
			$(".nav-tabs li.active").removeClass("active");
			t.addClass("active");
			var i = $(".nav-tabs li").index(t);
			$(".tab-content").hide().eq(i).show();
		}
	})
})