using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.MK
{
    public class MK_Runner
    {
        public static void Make_Scorpion_Stance_Left(MK_Main mk)
        {
            mk.Run(
                startX: 0, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels,
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_frame_0"
                );
            mk.Run(
                startX: (72 - 12) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_frame_1"
                );
            mk.Run(
                startX: (130 - 12) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_frame_2"
                );
            mk.Run(
                startX: (188 - 12) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_frame_3"
                );
            mk.Run(
                startX: 0, // x in bytes
                startY: (208 - 103) + 1, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_frame_4"
                );
            mk.Run(
                startX: (72 - 12) / 2, // x in bytes
                startY: (208 - 103) + 1, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name:  "scorpion_frame_5"
                );
            mk.Run(
                startX: (130 - 12) / 2, // x in bytes
                startY: (208 - 103) + 1, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_frame_6"
                );

        }

        public static void Make_Scorpion_Stance_Right(MK_Main mk)
        {
            mk.Run(
                startX: 0, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels,
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_R_frame_0",
                mirror: true
                );
            mk.Run(
                startX: (72 - 12) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_R_frame_1",
                mirror: true
                );
            mk.Run(
                startX: (130 - 12) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_R_frame_2",
                mirror: true
                );
            mk.Run(
                startX: (188 - 12) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_R_frame_3",
                mirror: true
                );
            mk.Run(
                startX: 0, // x in bytes
                startY: (208 - 103) + 1, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_R_frame_4",
                mirror: true
                );
            mk.Run(
                startX: (72 - 12) / 2, // x in bytes
                startY: (208 - 103) + 1, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_R_frame_5",
                mirror: true
                );
            mk.Run(
                startX: (130 - 12) / 2, // x in bytes
                startY: (208 - 103) + 1, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: "MEGAROM_PAGE_SCORPION_DATA_0",
                name: "scorpion_R_frame_6",
                mirror: true
                );

        }
    }
}
