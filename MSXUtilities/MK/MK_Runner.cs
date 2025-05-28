using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.MK
{
    public class MK_Runner
    {
        //public static void Make_Scorpion_Stance_Left(MK_Main mk, bool mirror)
        //{
        //    mk.Run(
        //        startX: 0, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: "scorpion_frame_0",
        //        );
        //    mk.Run(
        //        startX: (72 - 12) / 2, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: "scorpion_frame_1"
        //        );
        //    mk.Run(
        //        startX: (130 - 12) / 2, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: "scorpion_frame_2"
        //        );
        //    mk.Run(
        //        startX: (188 - 12) / 2, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: "scorpion_frame_3"
        //        );
        //    mk.Run(
        //        startX: 0, // x in bytes
        //        startY: (208 - 103) + 1, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: "scorpion_frame_4"
        //        );
        //    mk.Run(
        //        startX: (72 - 12) / 2, // x in bytes
        //        startY: (208 - 103) + 1, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name:  "scorpion_frame_5"
        //        );
        //    mk.Run(
        //        startX: (130 - 12) / 2, // x in bytes
        //        startY: (208 - 103) + 1, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: "scorpion_frame_6"
        //        );

        //}

        //public static void Make_Scorpion_Stance(MK_Main mk, string name, bool mirror)
        //{
        //    mk.Run(
        //        startX: 0, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: name + "_frame_0",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: (72 - 12) / 2, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: name + "_frame_1",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: (130 - 12) / 2, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: name + "_frame_2",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: (188 - 12) / 2, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: name + "_frame_3",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 0, // x in bytes
        //        startY: (208 - 103) + 1, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: name + "_frame_4",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: (72 - 12) / 2, // x in bytes
        //        startY: (208 - 103) + 1, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: name + "_frame_5",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: (130 - 12) / 2, // x in bytes
        //        startY: (208 - 103) + 1, // y in pixels
        //        width: 28, // in bytes
        //        height: 104, // in pixels
        //        megaROMpage: "MEGAROM_PAGE_SCORPION_STANCE_LEFT_DATA_0",
        //        name: name + "_frame_6",
        //        mirror: mirror
        //        );

        //    mk.SaveDataFile(name + "_frames_0_to_6");
        //}

        //public static void Make_Subzero_Stance_Frames_0_to_8(MK_Main mk, string name, bool mirror)
        //{
        //    mk.Run(
        //        startX: 0, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_0",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 25, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_1",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 50, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_2",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 75, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_3",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 100, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_4",
        //        mirror: mirror
        //        );

        //    mk.Run(
        //        startX: 0, // x in bytes
        //        startY: 110, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_5",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 25, // x in bytes
        //        startY: 110, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_6",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 50, // x in bytes
        //        startY: 110, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_7",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 75, // x in bytes
        //        startY: 110, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_0",
        //        name: name + "_frame_8",
        //        mirror: mirror
        //        );

        //    mk.SaveDataFile(name + "_frames_0_to_8");
        //}

        //public static void Make_Subzero_Stance_Frames_9_to_12(
        //    MK_Main mk,
        //    string name,
        //    bool mirror
        //    )
        //{
        //    mk.Run(
        //        startX: 0, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_1",
        //        name: name + "_frame_9",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 25, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_1",
        //        name: name + "_frame_10",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 50, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_1",
        //        name: name + "_frame_11",
        //        mirror: mirror
        //        );
        //    mk.Run(
        //        startX: 75, // x in bytes
        //        startY: 0, // y in pixels
        //        width: 25, // in bytes
        //        height: 107, // in pixels,
        //        megaROMpage: "MEGAROM_PAGE_SUBZERO_STANCE_RIGHT_DATA_1",
        //        name: name + "_frame_12",
        //        mirror: mirror
        //        );

        //    mk.SaveDataFile(name + "_frames_9_to_12");
        //}

        public static void Make_Subzero_Walking_Frames_0_to_8(
            MK_Main mk,
            string characterName,
            string position,
            string side
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.ToUpper(), side.ToUpper());

            mk.Run(
                startX: 28 * 0, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0
                );

            mk.Run(
                startX: 28 * 1, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: 28 * 2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );

            mk.Run(
                startX: 28 * 3, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 3
                );



            mk.Run(
                startX: 28 * 0, // x in bytes
                startY: 105, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 4
                );

            mk.Run(
                startX: 28 * 1, // x in bytes
                startY: 105, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 5
                );

            mk.Run(
                startX: 28 * 2, // x in bytes
                startY: 105, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 6
                );

            mk.Run(
                startX: 28 * 3, // x in bytes
                startY: 105, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 7
                );

            mk.Run(
                startX: 28 * 0, // x in bytes
                startY: 256, // y in pixels // first line of the second image
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 8
                );


            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 8,
                animationRepeatFrames: 3);
        }

        public static void Make_Scorpion_Walking_Frames_0_to_8(
            MK_Main mk,
            string characterName,
            string position,
            string side
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.ToUpper(), side.ToUpper());

            mk.Run(
                startX: 28 * 0, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0
                );

            mk.Run(
                startX: 28 * 1, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: 28 * 2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );

            mk.Run(
                startX: 28 * 3, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 3
                );



            mk.Run(
                startX: 28 * 0, // x in bytes
                startY: 105, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 4
                );

            mk.Run(
                startX: 28 * 1, // x in bytes
                startY: 105, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 5
                );

            mk.Run(
                startX: 28 * 2, // x in bytes
                startY: 105, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 6
                );

            mk.Run(
                startX: 28 * 3, // x in bytes
                startY: 105, // y in pixels
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 7
                );

            mk.Run(
                startX: 28 * 0, // x in bytes
                startY: 256, // y in pixels  // first line of the second image
                width: 28, // in bytes
                height: 106, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 8
                );


            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 8,
                animationRepeatFrames: 3);
        }

        public static void Make_Scorpion_Jumping_Up_Frames_0_to_2(
            MK_Main mk,
            string characterName,
            string position,
            string side
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.Replace('-', '_').ToUpper(), side.ToUpper());

            mk.Run(
                startX: 0, // x in bytes
                startY: 0, // y in pixels
                width: 54 / 2, // in bytes
                height: 108, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0
                );

            mk.Run(
                startX: (60 - 6) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 54 / 2, // in bytes
                height: 108, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: (116 - 6) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 54 / 2, // in bytes
                height: 108, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );


            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(keepUnderscores: false), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 2,
                animationRepeatFrames: 3);
        }

        public static void Make_Scorpion_Jumping_Forward_Frames_0_to_7(
            MK_Main mk,
            string characterName,
            string position,
            string side
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.Replace('-', '_').ToUpper(), side.ToUpper());

            mk.Run(
                startX: 0 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 44 / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0
                );

            mk.Run(
                startX: 50 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: 96 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );

            mk.Run(
                startX: 144 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 3
                );



            mk.Run(
                startX: 0 / 2, // x in bytes
                startY: 116, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 4
                );

            mk.Run(
                startX: 50/2, // x in bytes
                startY: 116, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 5
                );

            mk.Run(
                startX: 96/2, // x in bytes
                startY: 116, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 6
            );

            mk.Run(
                startX: 144 / 2, // x in bytes
                startY: 116, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 7
            );


            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(keepUnderscores: false), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 7,
                animationRepeatFrames: 3);
        }

        public static void Make_Subzero_Jumping_Up_Frames_0_to_2(
            MK_Main mk,
            string characterName,
            string position,
            string side
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.Replace('-', '_').ToUpper(), side.ToUpper());

            mk.Run(
                startX: 0, // x in bytes
                startY: 0, // y in pixels
                width: 54 / 2, // in bytes
                height: 108, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0
                );

            mk.Run(
                startX: (60 - 6) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 54 / 2, // in bytes
                height: 108, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: (116 - 6) / 2, // x in bytes
                startY: 0, // y in pixels
                width: 54 / 2, // in bytes
                height: 108, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );


            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(keepUnderscores: false), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 2,
                animationRepeatFrames: 3);
        }

        public static void Make_Subzero_Jumping_Forward_Frames_0_to_7(
            MK_Main mk,
            string characterName,
            string position,
            string side
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.Replace('-', '_').ToUpper(), side.ToUpper());

            mk.Run(
                startX: 0 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 44 / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0
                );

            mk.Run(
                startX: 50 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: 96 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );

            mk.Run(
                startX: 144 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 3
                );



            mk.Run(
                startX: 0 / 2, // x in bytes
                startY: 116, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 4
                );

            mk.Run(
                startX: 50 / 2, // x in bytes
                startY: 116, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 5
                );

            mk.Run(
                startX: 96 / 2, // x in bytes
                startY: 116, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 6
            );

            mk.Run(
                startX: 144 / 2, // x in bytes
                startY: 116, // y in pixels
                width: 44 / 2, // in bytes
                height: 52, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 7
            );


            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(keepUnderscores: false), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 7,
                animationRepeatFrames: 3);
        }

        public static void Make_Scorpion_Kick_Frames(
            MK_Main mk,
            string characterName,
            string position,
            string side
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.Replace('-', '_').ToUpper(), side.ToUpper());

            mk.Run(
                startX: 0 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 46 / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0
                );

            mk.Run(
                startX: 54 / 2, // x in bytes
                startY: 0, // y in pixels
                width: (104-54) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: 110 / 2, // x in bytes
                startY: 0, // y in pixels
                width: (148-110) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );

            mk.Run(
                startX: 156 / 2, // x in bytes
                startY: 0, // y in pixels
                width: (212-156) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 3
                );



            // high kick
            mk.Run(
                startX: 0 / 2, // x in bytes
                startY: 208-104, // y in pixels
                width: 78 / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 4
                );

            mk.Run(
                startX: 86 / 2, // x in bytes
                startY: 208 - 104, // y in pixels
                width: (156-86) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 5
                );

            // low kick
            mk.Run(
                startX: 160 / 2, // x in bytes
                startY: 208 - 104, // y in pixels
                width: (244-160) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 6
            );




            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(keepUnderscores: false), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 6,
                animationRepeatFrames: 3);
        }


        public static void Make_Scorpion_Block_Frames(
            MK_Main mk,
            string characterName,
            string position,
            string side
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.Replace('-', '_').ToUpper(), side.ToUpper());

            mk.Run(
                startX: 0 / 2, // x in bytes
                startY: 0, // y in pixels
                width: 60 / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0
                );

            mk.Run(
                startX: 62 / 2, // x in bytes
                startY: 0, // y in pixels
                width: (114 - 62) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: 116 / 2, // x in bytes
                startY: 0, // y in pixels
                width: (168 - 116) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );



            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(keepUnderscores: false), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 2,
                animationRepeatFrames: 3);
        }



        public static void Make_Scorpion_Crouching_Frames(
            MK_Main mk,
            string characterName,
            string position,
            string side,
            bool clearDataFile = false
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.Replace('-', '_').ToUpper(), side.ToUpper());

            mk.Run(
                startX: 0 / 2, // x in bytes
                startY: 107, // y in pixels
                width: 60 / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0,
                clearDataFile: true
                );

            mk.Run(
                startX: 62 / 2, // x in bytes
                startY: 107, // y in pixels
                width: (114 - 62) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 1
                );

            mk.Run(
                startX: 116 / 2, // x in bytes
                startY: 107, // y in pixels
                width: (168 - 116) / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 2
                );



            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(keepUnderscores: false), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 2,
                animationRepeatFrames: 3);
        }

        public static void Make_Scorpion_Crouching_Block_Frame(
            MK_Main mk,
            string characterName,
            string position,
            string side,
            bool clearDataFile = false
            )
        {
            var megaROMPage = String.Format("MEGAROM_PAGE_{0}_{1}_{2}_DATA_0", characterName.ToUpper(), position.Replace('-', '_').ToUpper(), side.ToUpper());

            mk.Run(
                startX: (200-12) / 2, // x in bytes
                startY: 107, // y in pixels
                width: 60 / 2, // in bytes
                height: 104, // in pixels,
                megaROMpage: megaROMPage,
                characterName: characterName,
                position: position,
                side: side,
                frameNumber: 0,
                clearDataFile: true
                );
            
            
            
            var temp = String.Format("{0}_{1}_{2}", characterName.ToPascalCase(), position.ToPascalCase(keepUnderscores: false), side.ToPascalCase());
            mk.SaveReferenceFiles(
                name: temp,
                firstFrame: 0,
                lastFrame: 0,
                animationRepeatFrames: 1);
        }

    }
}
