import {
    Component,
    Input,
    ElementRef,
    AfterContentInit,
    OnInit
} from '@angular/core';

import 'script-loader!smartadmin-plugins/datatables/datatables.min.js';

declare var $: any;

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'jq-datatable',
    template: `
        <table class='dataTable responsive {{tableClass}}' width='{{width}}'>
          <ng-content></ng-content>
        </table>
  `,
    // styles: ['../../../../../node_modules/smartadmin-plugins/datatables/datatables.min.css']
})
export class JqDatatableComponent implements OnInit {
    @Input() public options: any;
    @Input() public filter: any;
    @Input() public detailsFormat: any;

    @Input() public paginationLength: boolean;
    @Input() public columnsHide: boolean;
    @Input() public tableClass: string;
    @Input() public width = '100%';

    constructor(private el: ElementRef) { }

    ngOnInit() {
        this.render();
    }

    render() {
        const element = $(this.el.nativeElement.children[0]);
        let options = this.options || {};

        let toolbar = '';
        if (options.buttons) {
            toolbar += 'B';
        }
        if (this.paginationLength) {
            toolbar += 'l';
        }
        if (this.columnsHide) {
            toolbar += 'C';
        }

        if (typeof options.ajax === 'string') {
            const url = options.ajax;
            options.ajax = {
                url: url
                // complete: function (xhr) {
                //
                // }
            };
        }

        options = $.extend(options, {
            dom:
            // tslint:disable-next-line:max-line-length
            `<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs text-right'` + toolbar + `>r>` + `t` + `<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>`,
            oLanguage: {
                sSearch:
                // tslint:disable-next-line:quotemark
                "<span><strong>Search </strong></span> ",
                sLengthMenu: '_MENU_'
            },
            autoWidth: false,
            retrieve: true,
            responsive: true,
            initComplete: (settings, json) => {
                element
                    .parent()
                    .find('.input-sm')
                    .removeClass('input-sm')
                    .addClass('input-md');
            }
        });

        const _dataTable = element.DataTable(options);

        if (this.filter) {
            // Apply the filter
            element.on('keyup change', 'thead th input[type=text]', function () {
                _dataTable
                    .column(
                        $(this)
                            .parent()
                            .index() + ':visible'
                    )
                    .search(this.value)
                    .draw();
            });
        }

        if (!toolbar) {
            element
                .parent()
                .find('.dt-toolbar');
                // tslint:disable-next-line:max-line-length
                // .append('<div class="text-right"><img src="assets/img/logo.png" alt="SmartAdmin" style="width: 111px; margin-top: 3px; margin-right: 10px;"></div>');
        }

        if (this.detailsFormat) {
            const format = this.detailsFormat;
            element.on('click', 'td.details-control', function () {
                const tr = $(this).closest('tr');
                const row = _dataTable.row(tr);
                if (row.child.isShown()) {
                    row.child.hide();
                    tr.removeClass('shown');
                } else {
                    row.child(format(row.data())).show();
                    tr.addClass('shown');
                }
            });
        }
    }
}
