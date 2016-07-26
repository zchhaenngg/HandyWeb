$(function () {
    $('.module').hover(
        function () {
            var oriImgSrc = $(this).find('img').attr('src');
            var mousemoveImgSrc = oriImgSrc.replace('.png', '_Mouseover.png');
            $(this).find('img').attr('src', mousemoveImgSrc);
        },
        function () {
            var oriImgSrc = $(this).find('img').attr('src');
            var mouseoutImgSrc = oriImgSrc.replace('_Mouseover.png', '.png');
            $(this).find('img').attr('src', mouseoutImgSrc);
        }
    );

    $('.module').click(function () {
        $(this).find('a').get(0).click();
    });
});