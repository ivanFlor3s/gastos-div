import {
    Directive,
    ElementRef,
    EventEmitter,
    HostListener,
    Output,
} from '@angular/core';

@Directive({
    selector: '[clickOutside]',
})
export class ClickOutsideDirective {
    @Output() clickOutside = new EventEmitter<void>();

    constructor(private elementRef: ElementRef) {}

    @HostListener('document:click', ['$event.target'])
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    public onClick(target: any) {
        const clickedInside = this.elementRef.nativeElement.contains(target);
        if (!clickedInside) {
            this.clickOutside.emit();
        }
    }
}
