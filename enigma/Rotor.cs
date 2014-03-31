/*
 * Created by SharpDevelop.
 * User: AdrianClepcea
 * Date: 2/2/2005
 * Time: 12:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using BSKCrypto;
using System;
using System.Text;
using System.Windows.Forms;

namespace Enigma
{
	
	public class Rotor
	{
		private string layout;
		private byte offset;
		private Rotor previous, next;
		private Label lbl;
		private char cIn = '\0', notchPos;
        private String name;
	
		public Rotor(string layout,Label lbl,char notchPos, string nm)
		{
			this.layout = layout;
			this.previous = previous;
			this.next = next;
			this.lbl = lbl;
			this.notchPos = notchPos;
            this.name = nm;
			offset = 0;
			
		}
		
		public string GetLayout(){
			return layout;
		}
		
		public void SetNextRotor(Rotor next){
			this.next = next;
		}
		public void SetPreviousRotor(Rotor previous){
			this.previous = previous;
		}
		
		public char GetInverseCharAt(string ch){
			int pos = layout.IndexOf(ch);
			
			if(offset>pos){
				pos = 26 - (offset-pos);
			}else{
				pos = pos - offset;
			}
			
			if(previous!=null){
				pos = (pos+previous.GetOffset())%26;
			}
			
			return (char)(65+pos);
		}
		
		public int GetOffset(){
			return offset;
		}
		
		public char GetNotchPos(){
			return notchPos;
		}
		
		public void ResetOffset(){
			offset = 0;
            //Console.WriteLine("RESET OFFSET ROTOR " + name);
            LogForm.addLog("RESET OFFSET ROTOR " + name + ", POS:" + offset);
		}

        public void SetOffset(int off)
        {
            offset = Convert.ToByte(off);
            //Console.WriteLine("SET OFFSET ROTOR " + name);
            LogForm.addLog("SET OFFSET TO ROTOR " + name + ", POS:" + offset);
        }
		
		public bool HasNext(){
			return next!=null;
		}
		
		public bool HasPrevious(){
			return previous!=null;
		}
		
		public void Move(){
			if(next==null){
				return;
			}
			offset++;
			if(offset==26){
				offset = 0;
			}
			
			if(next!=null && (offset+66) == ((notchPos-64)%26)+66){
				next.Move();
			}
            //Console.WriteLine("MOVE ROTOR " + name);
            LogForm.addLog("MOVE ROTOR " + name + ", POS:" + offset);
			lbl.Text = ""+((char)(65+offset));
		}
		
		public void MoveBack(){
			if(offset==0){
				offset = 26;
			}
			offset--;

            //Console.WriteLine("MOVE BACK ROTOR " + name);
            LogForm.addLog("MOVE BACK ROTOR " + name + ", POS:" + offset);
			lbl.Text = ""+((char)(65+offset));
		}

        public void ResetLabel()
        {
            lbl.Text = "" + ((char)(65 + offset));
        }
		
		public void PutDataIn(char s){
			cIn = s;
			char c = s;
			c = (char)(((c - 65) + offset) % 26 + 65);
            //Console.WriteLine("PUTDATAIN ROTOR " + name);
            LogForm.addLog("PUTDATAIN TO ROTOR " + name + ": " + s);

			if(next!=null){
				c = layout.Substring((c-65),1).ToCharArray()[0];
				if((((c-65)+(-offset))%26 + 65)>=65){
					c = (char)(((c-65)+(-offset))%26 + 65);
				}else{
					c = (char)(((c-65)+(26+(-offset)))%26 + 65);
				}
				next.PutDataIn(c);
				
			}
		}
		
		public char GetDataOut(){
			char c = '\0';
            //Console.WriteLine("GETDATAOUT ROTOR " + name);

			if(next!=null){
				c = next.GetDataOut();
				c = GetInverseCharAt(""+c);
			}else{ //only in the reflector case
				c = layout.Substring((cIn-65),1).ToCharArray()[0];
				c = (char)(((c - 65) + previous.offset)%26+65);
				
			}
            LogForm.addLog("GETDATAOUT FROM ROTOR " + name + ": "+c);
			return c;
		}
		
	}
}
