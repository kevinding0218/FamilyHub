import { sequence, trigger, stagger,
    animate, style, group, query, transition,
    keyframes, animateChild, AUTO_STYLE, state
} from '@angular/animations';

export const notificationBottom = trigger('notificationBottom', [
    state('an-off, void',
        style({
            overflow: 'hidden',
            height: '0px',
        })
    ),
    state('an-animate',
        style({
            overflow: 'visible',
            height: AUTO_STYLE,
        })
    ),
    transition('an-off <=> an-animate', [
        animate('400ms ease-in-out')
    ])
]);
