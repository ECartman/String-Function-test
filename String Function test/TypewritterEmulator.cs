using System;
using System.Collections.Generic;
using System.Text;
namespace String_Function_test
{
    public class TypewritterEmulator
    {
        /**
          * the desired Characters to match for this tool
          */
        private static readonly char[] DEFAULT_DELIMITERS = { '\r', '\n' };

        /*
         * output styles, this are to be used to apply a way to send data into the buffer and process data to the "ultimate" data sink 
         * (example: a printer, a typewriter, a Terminal, you tell me! ) 
         * see each OuputStyle for details
         */
        public enum OuputStyle
        {
            /**
             * simulates a Typewritter, this means that whatever was "typed" unless Overwritten will be printed in place (same line) the only way to 
             * to implement this. you ignore the LF position and rather add the LF to the end of the Already existent String. 
             * clean it will be to remove the data by overwritting it. (adding a bunch of \0 or " ") 
             */
            TypeWritter,
            /**
             * simulates a terminal. hence if a LF is meet. and an new line is to be printed. whatever text Exists AFTER that LF will be loss and NOT 
             * printed
             * to implement this. you add the new line in place (meaning add the new line to the String) then not add new text, do not append or remove 
             * text that was added after that position. (the exact way will depend on how the process is implemented) this are just suggestions
             */
            Terminal_loss,
            /**
             *  if this style is applied YOU should Ensure that whatever text exist after the "LF" you mantain and use after the Text has been process by the LF
             *  for example: 
             *   Text String test\r\n second line is here hello!." 
             *   the tool should process "Text String test" and buffer or store " second line is here hello!." to be used on subsequent calls. or prints. 
             */
            Terminal_newLine
        };

        /**
         * the OuputStyle to use for this INSTANCE. 
         */
        private OuputStyle CurrentStyle = OuputStyle.TypeWritter;
        /**
         * the Line input position on the Buffer. if -1 means that the
         * value is to be appended into the buffer. 
         */
        private int LineInputPos = -1;

        /**
         * a buffer that is HYPOTETICALLY EXTREMLY SLOW. 
         * ADDING data to this Buffer imply task is REALLY slow. hence you should ONLY add whatever is REALLY to be printed. 
         * make the following asumption: 
         * This HYPOTETICAL <whatever> is a object that writes strings into a YE OLD electrical typewritter 
         * to add to the challenge see https://www.youtube.com/watch?v=5_6aGJcHeZQ
         * hence. as you might have seen there. typing takes time. 
         * ERASING a Letter takes 3 times the time it takes to write a single character 
         * and the Erasing tape. is limited.  
         * hence note that Adding data to this buffer is "slow" and only do when data is ensured to be typed. 
         * 
         * to add extra challenge. lets make another asumption: 
         * RAM is limited. you are not allowed to create or use a secondary buffer. (otherwise you are circumventing the original challenge) 
         * hence you are not allowed to crea a second "StringBuilder" Global. Nor "StringBuilder" local 
         * you are not to create Substrings (unless you are about to commit them to the Buffer) 
         * nor are you allowed to use Character arrays. 
         */
        private static readonly StringBuilder LineBuffer = new StringBuilder(1024);

        private bool changeOuputStyle(OuputStyle newStyle)
        {
            //we are not allowing this call just yet. we need to ensure the code
            //works fine when flushing and printing with other style. that is not yet ready... 
            //hence lists throw what is apropiate. 
            throw new NotImplementedException("TODO");
            //CurrentStyle = newStyle;
        }

    /**
     * TO BE REMOVED> or enhanced.
     * THIS IS A TEST FUNCTION TO RESET THE BUFFER & LineInputPos
     * 
     */
    public string reset()
    {
        LineInputPos = -1;
        string deleteddata = LineBuffer.ToString();// here we break the rune as this is not part of the challenge. we basically want this for  analisis. 
        LineBuffer.Clear();
        return deleteddata;
    }


    //////////
    /// TODO: Re-Implement using OuputStyle's
    /// the restriction we set. this will be SLOW 
    //////////
    /**
    * the fools proof method. 
    * fast, short. elegant? (there are a few details that are not quite there... but in general is fine.)
    * 
    * also a few grains of knowledge: 
    * in general and with no restrictions this is usually better. 
    *          due simplicity and performance wise there is little to BE gain. 
    * C# optimizations causes that "string[X]" is a extern gets the info really fast and the implemtation is hidden from simpletons that read code.
    * with no access to the secret Sause of C#. (we can seek on git.. but pass.) i presume this pulls the VALUE from the String Array (characters) refence. 
    * 
    * sufice to say. "string[X]" is really fast to get the value from memory. to the point is fast get a value  (not a refence) so is a "copy" but fast... 
    * 
    * if we compare the hoops we took to read the data from the string this is faster. BUT there is 1 scenario where our aproach is faster. 
    * LONG strings with many CR and several Lines. 
    * on such scenarios our method performs better for a similar scenario. 
    * we dont write to memory. we just seek the points where there is a delimiter. (either CR or LF)  
    * and this could even be faster. if we had some restrictions for strings.and we mapped them. but that require more usage of memory. and that is the big no. 
    * 
    * this example is to show how to work with some restrictions. 
    * i.e: 
    *  memory is a EXTREMLY limited resource in our... space ship or our Raspeby is uber demanded for Ram and we should avoid using too much on the buffer... 
    */
    public int OutputOriginal(string Text)
    {
        LineInputPos = 0;
        for (int i = 0; i < Text.Length; ++i)
        {
            char c = Text[i];
            if (c == '\n')
            {
                LineBuffer.AppendLine();// --> this will ALWAYS use TypeWritter OuputStyle given this line of code. Extra challenge. (implement the diferent OuputStyle's) 
                ProcessLine(LineBuffer);
                LineBuffer.Clear();
                LineInputPos = 0;
            }
            else if (c == '\r')
            {
                LineInputPos = 0;
                //if next char is \n the prev code will be executed... but... i dunno. i think this Could be better. why   LineInputPos = 0; instead if just flushing? wasting 1 calculation
            }
            else
            {
                if (LineInputPos >= LineBuffer.Length)
                {
                    LineBuffer.Append(c);
                }
                else
                {
                    LineBuffer[LineInputPos] = c;
                }
                ++LineInputPos;
            }
        }
        if (LineInputPos == 0)
            LineInputPos = -1;
        return 0;
    }

    /**
    * "print to console what is on the typewritter buffer. 
     */
    private void ProcessLine(StringBuilder stringBuilder)
    {
        // stringBuilder.ToString();
        Console.Write(stringBuilder.ToString());
    }


    /**
     * Output Function: 
     *  this function will parse the @param Text and will parse its content by lines 
     *  (lines define by the LF character / EOL / "\n" )
     *  NOTE: if the string provides not new lines we will just buffer the string. and not print it. until a new call is done and a LF is provided.
     *  or Reset is called/done 
     *  
     *  this function will also process the CR (carret Return) character and avoid adding data that is to be removed.
     *  if CR is provided but there is not enoght data we will do as much as posible and allow subsecuent calls to overwrite data (which unfortunately
     *  imply doing "slow remove") 
     *  
     *  (aka: we do either terminal emulation (Basic) rather than typewritter emulation (double print strings, or showing letter on top of other letters)
     *  
     *  this is done in order to compute the text we need to copy instead of just copy and copy again (wasting time) and memory.
     *  if the imediate charater to CR is LF this means is a "Environment.NewLine" hence we basically remove the CR and work as if it were a EOL only
     *  
     *  if provided null or empty string this function returns imediately. 
     *  (TODO:  either add something for that scenario. or blame the caller. make him pay how dare to send nulls or empty strings!)
     *  
     *  @param Text to parsed. 
     *  
     */
    public void Output(string Text)
    {
        if (string.IsNullOrEmpty(Text)) return;// we should throw a exception here just to annoy the caller!, how dare not to check the string.
                                               //check and get the delimiter of the String.
        int delimiterIndex = Text.IndexOfAny(DEFAULT_DELIMITERS);
        int StartLineIndex = 0;
        do
        {
            if (delimiterIndex < 0)
            {
                ProcessEOS(LineBuffer, Text, ref LineInputPos, StartLineIndex);
                StartLineIndex = Text.Length;
            }
            else
            {
                switch (Text[delimiterIndex])
                {
                    case '\n':
                        ProcessEOL(LineBuffer, Text, ref LineInputPos, StartLineIndex, delimiterIndex);
                        break;
                    case '\r':
                        {
                            // is the last char on the string? if so, we need to setup for next call to this function. 
                            if (delimiterIndex + 1 == Text.Length)
                            {
                                if (LineInputPos == -1)
                                    LineBuffer.Append(Text, StartLineIndex, delimiterIndex - StartLineIndex);
                                else
                                    InsertSubstringinto(LineBuffer, Text, LineInputPos, StartLineIndex, delimiterIndex - StartLineIndex);
                                LineInputPos = 0;// we "flag" the line can be overwritten starting from postion 0 of the line... 
                            }
                            else if (Text[delimiterIndex + 1] == '\n')//CRLF or Windows "Environment.NewLine" 
                            {
                                ProcessEOL(LineBuffer, Text, ref LineInputPos, StartLineIndex, delimiterIndex);
                                ++delimiterIndex;//consume the LF character. 
                            }
                            else
                            {
                                ProcessCR(LineBuffer, Text, ref LineInputPos, StartLineIndex, ref delimiterIndex);
                            }
                        }
                        break;
                }
                StartLineIndex = ++delimiterIndex;
            }
        }
        //get the next index of delimiter and loop if any found OR our StartLineIndex is not the end of the string (we still pending some text to process) 
        while ((delimiterIndex = Text.IndexOfAny(DEFAULT_DELIMITERS, StartLineIndex)) > -1 || StartLineIndex < Text.Length);
    }


    /**
     * Process the End Of String. 
     */
    private void ProcessEOS(StringBuilder Builder, string Text, ref int Builder_InputPos, int StartLineIndex)
    {
        if (StartLineIndex >= Text.Length)
        {
            //a start index of the lenght of the String(or more... why? ) means either we reach the String end. AND alredy process the string
            //OR
            //we dont want to add anything. hence return. 
            return;
        }
        //There are no CR or LF or CRLF from StartLineIndex. hence. we should buffer the string from StartLineIndex;

        if (Builder_InputPos > -1 && Builder.Length - Builder_InputPos <= (Text.Length - StartLineIndex))
        {
            //here we need to delete the whole "rest of the string" from the buffer... this is "slow"... but we had no alternative 
            Builder.Remove(Builder_InputPos, Builder.Length - Builder_InputPos);
            //the rest of text will be appended. and we will overwrite the chunk of data that was avail hence. we dont need Builder_InputPos
            Builder_InputPos = -1;
        }
        if (Builder_InputPos > -1)
        {
            var prevLength = Builder.Length;
            var count = Text.Length - StartLineIndex;
            /*
            if (StartLineIndex == 0) {
                //TODO: Profile this if this is slowER than InsertSubstringinto as this are 2 task removal then writting. we could use InsertSubstringinto to do both at once.
                //looking at the source this seems to be slower... as the internal CharArray needs to restructure... hence it would see as if InsertSubstringinto might be faster. 
                Builder.Remove(Builder_InputPos, Text.Length);
                Builder.Insert(Builder_InputPos, Text);
            }
            else
            */
            InsertSubstringinto(Builder, Text, Builder_InputPos, StartLineIndex, count);
            Builder_InputPos += count;
            if (Builder_InputPos >= prevLength)
            {
                //we ovewritted all the chunks of data avaiable on the buffer. new data is to be appended not "inserted or overwritte"
                Builder_InputPos = -1;
            }
        }
        else
        {
            if (StartLineIndex == 0)
                Builder.Append(Text);
            else
                Builder.Append(Text, StartLineIndex, Text.Length - StartLineIndex);
        }
    }

    /**
    * Process the End Of Line. 
    * TODO: finish the OuputStyle(s) implmentations for all that are not TypeWritter
    */
    private void ProcessEOL(StringBuilder Builder, string Text, ref int Builder_InputPos, int StartLineIndex, int delimiterIndex)
    {
        if (Builder_InputPos == -1)
        {
            Builder.Append(Text, StartLineIndex, delimiterIndex - StartLineIndex);
            LineBuffer.AppendLine();
        }
        else
        {
            if (CurrentStyle == OuputStyle.TypeWritter)
            {
                InsertSubstringinto(Builder, Text, Builder_InputPos, StartLineIndex, delimiterIndex - StartLineIndex);
                LineBuffer.AppendLine();
            }
            else
            {
                //In Terminal style SHOULD NOT just ".AppendLine()" rather we should
                //check if Insert is Pending we should Insert Environment.NewLine THEN we should split the Buffer. 
                // then flush up to Builder_InputPos then reset LineInputPos and insert the splitted text to the buffer... 
                //someone it seems that the original creator of the tool either did not saw this or ignored that. 
                //we do too to simulate the same behaviour. 
                var prevLength = Builder.Length;
                InsertSubstringinto(Builder, Text, Builder_InputPos, StartLineIndex, delimiterIndex - StartLineIndex);
                Builder_InputPos += delimiterIndex - StartLineIndex;
                if (Builder_InputPos >= prevLength)
                {
                    //we ovewritted all the chunks of data avaiable on the buffer. new data is to be appended not "inserted or overwritte"
                    Builder_InputPos = -1;
                    LineBuffer.AppendLine();
                }
                else
                {
                    InsertSubstringinto(Builder, Environment.NewLine, Builder_InputPos, 0, Environment.NewLine.Length);
                    if (CurrentStyle == OuputStyle.Terminal_newLine && Builder_InputPos + Environment.NewLine.Length < prevLength)
                    {
                        //Terminal_newLine tell us that we need to preseve the data. we do so on the buffer. 
                        //to do so we need to create a temporal buffer. to use a sub-buffer. and have the low notes... i mean text... 
                        Builder_InputPos += Environment.NewLine.Length;
                        //TODO: MAKE A SUBBUFFER OR HAVE A WAY TO MAKE TEXT SURVIVE AFTER IT HAS BEEN FLUSH I HAVE IDEAS BUT THIS EXAMPLE DOES NOT MERIT 
                        //MAKING ALL THAT CODING FOR A EXTREMLY UNLIKELY SCENARIO XD 
                    }
                }
            }
        }
        //TODO: if this is NOT corrected. this will NOT print a single line. rather will print at least 2. a second buffer to retain the excess from a line might be needed. 
        //not done due we are simulating the original code we based this one uppon
        // I am at this point tired of making more fixes to this code. hence put a pin here and future me correct this thing... 
        ProcessLine(Builder);
        //IF we Corrected the above. we need to refil the Buffer with the pending data. do that instead of Clear OR refill? not sure... 
        Builder.Clear();
        Builder_InputPos = -1;//reset the CR position as we reset the buffer.
    }

    /*
    * a problem here: 
    * 
    * String Builder Lacks a function that I PERSONALLY think it should have:
    * 
    *          oversight from C# folks.Replace function should have a  Replace(Startindex, EndIndex, String Remplacement,Count)  or even more options.
                //hence to replace chunks not single characters. or single instances of specific strings. 
                // see this scenario for example. we need to replace a start index. up to the size of the new string. 
                // hence we need to call remove ourselves then insert the string. there is not remplacement option.... 
    *  
    * why this is a problem: 
    * when we create a Substring OR a call ToCharArray the undertline code copy the substring into into a new Object.(this is intended due the desire to have immutability on Strings)
    * and our goal is to move the text from String to Stringbuilder not to make 3 copies, and waste memory and time. 
    * 
    * an example: 
    *               string<original> --> (Substring| ToCharArray Creates a copy )--> string<sub>[this is a copy that was made to a diferent section of memory] --> feed into StringBuilder --> copy underline pointers data (AGAIN!)
    *               
    *              
    * note String does not have a function to get a reference of the underline character data.(this is by design) however Functions such as those on Builder or String itself should have something to 
    * ensure performance for their clases (Builder has for SOME of their methods, but as suggested. there is once scenario missing. 
    * there is not a way to include a PARTIAL String. (to Insert a string with an Index, with a specific string from one point to another.) 
    * but that is to be suggested. what todo to solve right now? 
    * 
    * we can add our own code to do the "unsafe" movement
    * otherwise the other way is to do or use 
    * StringBuilder.remove( <> ) 
    * then
    * StringBuilder.Insert(LineInputPos, Text.Substring(Index, Count));
    * StringBuilder.Insert(LineInputPos, Text.ToCharArray(Index, Count));
    *
    */
    private unsafe static void InsertSubstringinto(StringBuilder LineBuffer, string Text, int bufferposition, int startindex, int count)
    {
        //original implementation: 
        //LineBuffer.Insert(LineInputPos, Text.ToCharArray(), startindex, count);
        // or
        // LineBuffer.Insert(LineInputPos, Text.ToCharArray(startindex, count));
        // but... we choose PAIN and suffering in exchange of ensuring we are not "copying" a substring rather we copy values
        // a fun fact. using String.this[X] BASICALLY does the same thing. but i used this to learn more about how c# handle refernce and pointers. 
        //fun stuff. 


        if (startindex < 0 || startindex > Text.Length || startindex > Text.Length - count)
        {
            throw new ArgumentOutOfRangeException("startIndex", "Index out of Bounds");
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException("length", "Index out of Bounds");
        }

        //ensure the Buffer has enought space: 
        if (LineBuffer.Capacity < bufferposition + count)
        {
            throw new OutOfMemoryException("BufferCapacity Exceeded");
        }

        fixed (char* textptr = Text)
        {
            char* charPointer = textptr + startindex;
            /* 
             * why do this? well we want to avoid *copy* data from one String into a Substring. as this is doing the work 3 times: 
             * <Original string> 
             *  <Original string>.tocharArray(index, size)   or   <Original string>.substring(index, size) --> <Substring copy>
             * < LineBuffer>.Insert/append(Position,<Substring copy>) --> copy data from <Substring copy> into < LineBuffer>
             * we want to avoid the need to create <Substring copy>. it takes time and memory that we could use elsewhere. 
             * hence we FOR THIS IMPLEMENTATION is critical for us. hence lets gather the chars directly from the original source (DIRECTLY not as a copy) 
             * 
             * 
             * C# COPY the string using  Buffer.Memcpy() [SEE https://docs.microsoft.com/en-us/dotnet/api/system.buffer.memorycopy?view=net-6.0 ]
             * at THIS point we dont want a copy. we rather want to avoid actively making a copy. as we desire to insert into the buffer (internally the buffer copy the data into its own mem.
             * hence using sub or array is creating a 3th copy for no reason instead of 2 ... immutability... what a pain... 
             * 
             */

            for (var charscopied = 0; charscopied < count; charscopied++)
            {
                if (bufferposition == LineBuffer.Length) // we dont change the lenght here as this imply adding '\0' chars and that takes time. appending is faster (as changing the lenght also calls append " Append('\0', num);" )
                {
                    LineBuffer.Append(*charPointer);
                    bufferposition++;
                }
                else
                {
                    LineBuffer[bufferposition++] = *charPointer;
                }
                charPointer++;
            }
        }
    }

    /**
     * Process The CR character (Carriage Return) 
     * 
     * This could be shorter and move some task to the other delimiter functions... 
     * i.e: 
     * have this only to feed a stack (becoming the stack a Global. ) 
     * 
     * 
     *  well we have a CR, but is not the last piece of text nor is followed by LF.
     * MATH TIME..
     * 
     * we want to avoid a scenario where we loop each character. and copy (wastefully so) 
     * into the buffer. as it takes computation and time and our buffer is "slow"
     * 
     * for example. 
     * we want to avoid a scenario where  we add 3 sentences to the buffer. then delete 2 due a CR character.
     * ask to delete previous text. 
     * we want to anticipate and just add the text that will be actually be printed. 
     * here is posible for callers to send text that contain Multiple CR for exmaple: 
     * 
     * 
     * Lorem\r ipsum dolor sit \ramet, consectetur adipiscing elit, sed do eiusmod\r
     * or
     * "met, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r"
     * 
     * what if the first text is longer. the second shorter and the third in between? 
     * here the problem get hard, we could just interate and overwrite text. but that imply wasting computation (i/o) to do that. 
     * we take data from one place, to WRITE to another that then we remove. wasting time. (ram... well not a big deal but WHAT IF 
     * this is going to disk? even worse what IF this is going to a legacy device i.e magnetic ? ) 
     * (copy from memory. to other place of memory, then disregard the previous task as we will overwrite. and so on. )
     * (or even worse add a new buffer to prebuffer the buffer.) 
     * we can do better. but well. we need to write math. and math is hard... 
     * so far we know the "OverridableChars" that are the characters from (inclusive) StartLineIndex up to (exclusive) delimiterIndex
     * we need to determine the "chunk of text" that will be written (to overwritte on the string we will buffer) 
     * now CR is not deletion of the hole line. NOR is a "new line" (at least not on Modern OS's) 
     * the CR means go back to the begining of the line. then whatever is provided "write" OVER whatever was before. 
     * this is to "simulate" a ye old typewritter. (I have one if a demo is required HA! )
     * ANYWAY. with a known "next chunk of text" a problem might arrise. 
     * what if our caller Asked for yet another CR and we need yet to overwritte back again. 
     * 
     * <example>
     * example: 
     * 
     * 
     * chuncks: 
     * "met, consectetur adipiscing elit, sed do eiusmod\r"
     * "Lorem\r"
     * "ipsum dolor sit \r"
     *  
     * so we have: 
     * "░░░░░consectetur adipiscing elit, sed do eiusmod"
     * "░░░░░" --> will be replaced with Lorem
     * 
     * ok, so far so good but. will this also be overwritten later on. hence:
     * 
     * "░░░░░░░░░░░░░░░░ adipiscing elit, sed do eiusmod"
     * "ipsum dolor sit  adipiscing elit, sed do eiusmod" 
     * (AND since we end in \r we mark this string to ve Overwritten if needs be but as we are in the end of the string we cannot overwrite further until next input)
     * 
     * 
     * </example>
     * 
     * I hope that shows the complexity of the problem to solve. we have a posibility for a single string to have several CR. as matter of fact. 
     * a Caller can be a prick and send us something like 
     * "haha I am a prick\r\r\rHaJA\rJA\r\n" --> (this code does not check for this scenarios we can enhance checking for "\r\r" or where \r is follow by a delimier.. etc
     * 
     */
    private static void ProcessCR(StringBuilder Builder, string Text, ref int Builder_InputPos, int StartLineIndex, ref int delimiterIndex)
    {
        /*
                             * so we have: 
                             * "░░░░░consectetur adipiscing elit, sed do eiusmod"
                             * "░"-> to be overwritten. 
                             * "Loremconsectetur adipiscing elit, sed do eiusmod"
                             * next is "\r\n" or a "Windows" LF. yet the old code process this per char (funny enought) 
                             * hence lets simulate that. 
                             * the whole line is "overwrittable." but we have a line feed so ignore that and flush the string to underline output. (i.e: console) 
                             * BUT!!!!
                             * what happends to "ipsum dolor sit" , this is on a later loop cycle process on the string. 
                             * but note: 
                             * the actual string should be: 
                             * "Lorem\r\nconsectetur adipiscing elit, sed do eiusmod" 
                             * or
                             * "Lorem"
                             * and another string: 
                             * "consectetur adipiscing elit, sed do eiusmod"
                             * why: 
                             * because technicall the \r\n is at the end of "Lorem" 
                             * instead the old code version did: 
                             * "Loremconsectetur adipiscing elit, sed do eiusmod\r\n" (added the new line at the end of the text... ) 
                             * it seems this was desired. yet incorrect way to do the CR because basically ignores "LF"
                             * </example>
                             * 
                             * so TL;DR... it seems that it was A Design choice to ignore when LF exists somepoint in the midle of a String and instead if a LF is found 
                             * regardless where it is is process as if LF is a command to "hey print the string at current state regardless where the LF is, and add the LF at the end"
                             * 
                             * 
         */
        //this can be as little as 1 CR on a String and only god know what the caller might be doing hence... a Stack to track
        //why a stack? because we want to queue items. and when we have them all. our resulting string will be made out of the last first and up the the newest.
        //what i want here. is to store in a stack a PAIR of values. a index and "count"
        Stack<int[]> CRindexes = new Stack<int[]>();
        // include the index of the String start and the count of characters.  on the bottom of a stack
        CRindexes.Push(new int[] { StartLineIndex, delimiterIndex - StartLineIndex });
        int NextDelimiter = delimiterIndex;
        do
        {
            delimiterIndex = NextDelimiter;
            NextDelimiter = Text.IndexOfAny(DEFAULT_DELIMITERS, NextDelimiter + 1);
            //if no other Delimiter were Found.
            //Or
            //a new line is detected (either LF or CRLF)
            //Or
            //if. the CR is the last character on the string
            //the we process the text and bail. 
            if (NextDelimiter < 0 ||
                Text[NextDelimiter] == '\n' ||
                NextDelimiter + 1 == Text.Length ||
                (Text[NextDelimiter] == '\r' && NextDelimiter + 1 < Text.Length && Text[NextDelimiter + 1] == '\n'))
            {
                //exclude the "CR, LF, or CRLF" to be written on the string. 
                CRindexes.Push(new int[] { delimiterIndex + 1, (NextDelimiter < 0 ? Text.Length : NextDelimiter) - (delimiterIndex + 1) });
                //ok. on either of this cases we want to add the text(s) we have into the buffer. 
                int Overwrittencount = 0;
                while (CRindexes.Count > 0)
                {
                    var pair = CRindexes.Pop();
                    /*write only make sence if the Text is more than the alredy written text (we are writting newer to older) 
                     * hence here we desire to only write into our buffer those strings that will be displayed. on the string 
                     * and not commit the same mistake as a "real" typewritter and print letters in top of other letter 
                     * or in this case wasting time writting unless data 
                     * see it this way: 
                     * using the same example as before: 
                     * "met, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r"
                     * chuncks: 
                     * "met, consectetur adipiscing elit, sed do eiusmod\r"
                     * "Lorem\r"
                     * "ipsum dolor sit \r"
                     * 
                     * when they get added into the stack the seem as such: 
                     * "ipsum dolor sit "  --> the top string on the stack as this one was the last added and fist pooped by the stack
                     * "░░░░░" -> we basically skip this one as it has not data that will be printable(seen) [NOTE:] we could even remove it when the next item on the stack is included. perfoance however gains little
                     * "░░░░░░░░░░░░░░░░ adipiscing elit, sed do eiusmod"  -> since this will be printed. we just include the non removed data to the buffer
                     * then given the whole string end in CR we mark it to be overwrittable.
                     * ALSO: 
                     * note: the stack Does NOT contain Strings. just pair of cordinates on where the data is on the string. 
                     * this is aking to Indexing. which brinds me to another topic. 
                     * what is gain by this hole ordeal? at the end the string is navigated either way by "IndexOfAny"
                     * true. However is done on a faster scope. either at native CLR (C/C++)  OR even directly at some algorithm that is faster using advantages of the CPU... 
                     * yes perhaps i am asumming Lang devs are too good. and is even posible they are iterating 1 by 1 each character... 
                     * but at the least i know that copy and inserts are done at native level as they use unsafe methods (unsafe in the sence they use native points etc.)
                     * 
                     * my test however suggest that the gain is TOO little. there is indeed but little... at least for "small strings"
                     * HOWEVER the aproach is great for string that hace 1 or none seek chars as those take o(N)=n at worse and at best is log? (depends on how Indexof is actually implemented) 
                     * but seems to be faster than manual iteration.
                     * 
                     * well at the least i found a "OK way" to do this... 
                     * however it consumed a lot of time breaking my head to resolve a non problem... 
                     * and did not get much on o(N) performance... 
                     * 
                     */

                    if (CRindexes.Count == 0 && Builder_InputPos > 0 ? pair[1] + Builder_InputPos > Overwrittencount : pair[1] > Overwrittencount)
                    {
                        //only overwrite the text that needs to be added. note we use a stack. hence the first iteration will bring the last text to add
                        //hence the next iteration we dont need to overwrite the text. we need to 
                        //also we will ignore Builder_InputPos if we find more than a single CR as that means Builder_InputPos is indeed irelevant we only care for it IF there is(was) only 1 CR
                        if (Builder_InputPos == -1)
                        {
                            Builder.Append(Text, pair[0] + Overwrittencount, pair[1] - Overwrittencount);
                        }
                        else
                        {
                            var prevLength = Builder.Length;
                            if (CRindexes.Count == 0)
                            {
                                InsertSubstringinto(Builder, Text, Overwrittencount, pair[0] + Overwrittencount - Builder_InputPos, (pair[1] - Overwrittencount) + Builder_InputPos);
                            }
                            else
                            {
                                InsertSubstringinto(Builder, Text, Overwrittencount, pair[0] + Overwrittencount, pair[1] - Overwrittencount);
                            }

                            if (Builder_InputPos >= prevLength || Overwrittencount > prevLength)
                            {
                                //we ovewritted the CR text alredy. no need to keep track of LineInputPos any new string will be appended
                                Builder_InputPos = -1;
                            }
                        }
                        Overwrittencount = pair[1];
                    }
                }
                //dont want to handle the delimiters(new line or end of string) here. lets loop and let the rest of the code handle that.
                //WITH ONE EXCEPTION if we reached the EOS
                Builder_InputPos = (NextDelimiter < 0 ? Text.Length : NextDelimiter) - (delimiterIndex + 1);// the ammount of chars that where written before the delimiter. if a LF was provided this will be changed on the next loop 
                delimiterIndex = (NextDelimiter < 0 ? Text.Length : NextDelimiter) - 1;
                break;//break from the loop;
            }
            else
            {
                //a CR was found. AND is not any of the exit conditions. add to the stack.
                // the index of the letter AFTER CR is our start point up to the next delimiter 
                CRindexes.Push(new int[] { delimiterIndex + 1, NextDelimiter - (delimiterIndex + 1) });
            }
        } while (true);
    }
}
}
