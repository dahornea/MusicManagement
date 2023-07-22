import { Artist } from "./Artist";
import { RecordLabel } from "./RecordLabel";

export interface Certification {
    Award: string;
    UnitsSold: number;
    ArtistId?: number;
    RecordLabelId?: number;
    Artist?: Artist;
    RecordLabel?: RecordLabel;
    [key: string]: any;
}