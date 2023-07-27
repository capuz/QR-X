$(document).ready(function() {

var config = { 
    '.chosen-select': {placeholder_text_multiple : '[ Todos ]' },
    '.chosen-select-deselect': { allow_single_deselect: true },
    '.chosen-select-no-single': { disable_search_threshold: 3 },
    '.chosen-select-no-results': { no_results_text: 'Oops, no se encuentra!' },
}
for (var selector in config) {
    $(selector).chosen(config[selector]);
}
});