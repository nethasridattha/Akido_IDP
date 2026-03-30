import { JourneyResult, Status, ErrorCode } from './assets/web-sdk-dx-dxh41c99caf4ecfe71.js';
export type Language = 'en' | 'ar' | 'ru' | 'fr' | 'zh' | 'it' | 'tr' | 'uk';
export type Theme = 'light' | 'dark' | 'system';
export type Gesture = 'blink' | 'smile' | 'turnLeft' | 'turnRight';
export type DocumentType = 'passport' | 'emirates_id' | 'gcc_id';
export type FacingModeType = 'user' | 'environment' | undefined;
export interface UaeKycConfigOptions {
    language?: Language;
    privacyPolicyUrl?: string;
    logoUrl?: string;
    theme?: Theme;
    journeyToken: string;
    apiDomain?: string;
    accentColor?: string;
    docCaptureCamFacingMode?: FacingModeType;
}
export type UaeKycCallback = (result: JourneyResult) => void;
/**
 * Start the KYC journey using an iframe
 * @param config - Configuration options for the KYC journey
 * @param callback - Callback function to be called when the journey is complete
 * @returns Promise<void>
 */
export declare function startJourney(config: UaeKycConfigOptions, callback: UaeKycCallback): Promise<void>;
export { Status, ErrorCode };
interface UaeKycSdk {
    startJourney: typeof startJourney;
    Status: typeof Status;
    ErrorCode: typeof ErrorCode;
}
declare global {
    interface Window {
        UAEKYC: UaeKycSdk;
    }
}
