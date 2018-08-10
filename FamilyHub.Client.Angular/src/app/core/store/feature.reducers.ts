import * as fromApp from './app.reducers';
import { MemberState } from './member/member.reducers';

export interface FeatureState extends fromApp.AppState {
    members: MemberState;
}
