
namespace Moons.Common20.CRC
{
	/// <summary>
	/// 索罗门校验
	/// </summary>
	public class SOLOMN
	{ 
		#region "数组定义"

		private byte[] ceolongitude = new byte[]
			{
				174,173,172,171,169,168,167,165,164,162,
				161,159,157,156,154,152,150,148,146,144,
				142,140,138,136,133,131,129,127,124,122,
				120,117,114,112,109
			};
		private byte[] exp_table = new byte[]
			{
				1,   2,   4,   8,  16,  32,  64, 128,  29,  58, 116, 232, 205, 135,  19,  38,
				76, 152,  45,  90, 180, 117, 234, 201, 143,   3,   6,  12,  24,  48,  96, 192,
				157,  39,  78, 156,  37,  74, 148,  53, 106, 212, 181, 119, 238, 193, 159,  35,
				70, 140,   5,  10,  20,  40,  80, 160,  93, 186, 105, 210, 185, 111, 222, 161,
				95, 190,  97, 194, 153,  47,  94, 188, 101, 202, 137,  15,  30,  60, 120, 240,
				253, 231, 211, 187, 107, 214, 177, 127, 254, 225, 223, 163,  91, 182, 113, 226,
				217, 175,  67, 134,  17,  34,  68, 136,  13,  26,  52, 104, 208, 189, 103, 206,
				129,  31,  62, 124, 248, 237, 199, 147,  59, 118, 236, 197, 151,  51, 102, 204,
				133,  23,  46,  92, 184, 109, 218, 169,  79, 158,  33,  66, 132,  21,  42,  84,
				168,  77, 154,  41,  82, 164,  85, 170,  73, 146,  57, 114, 228, 213, 183, 115,
				230, 209, 191,  99, 198, 145,  63, 126, 252, 229, 215, 179, 123, 246, 241, 255,
				227, 219, 171,  75, 150,  49,  98, 196, 149,  55, 110, 220, 165,  87, 174,  65,
				130,  25,  50, 100, 200, 141,   7,  14,  28,  56, 112, 224, 221, 167,  83, 166,
				81, 162,  89, 178, 121, 242, 249, 239, 195, 155,  43,  86, 172,  69, 138,   9,
				18,  36,  72, 144,  61, 122, 244, 245, 247, 243, 251, 235, 203, 139,  11,  22,
				44,  88, 176, 125, 250, 233, 207, 131,  27,  54, 108, 216, 173,  71, 142,   1,
				2,   4,   8,  16,  32,  64, 128,  29,  58, 116, 232, 205, 135,  19,  38,  76,
				152,  45,  90, 180, 117, 234, 201, 143,   3,   6,  12,  24,  48,  96, 192, 157,
				39,  78, 156,  37,  74, 148,  53, 106, 212, 181, 119, 238, 193, 159,  35,  70,
				140,   5,  10,  20,  40,  80, 160,  93, 186, 105, 210, 185, 111, 222, 161,  95,
				190,  97, 194, 153,  47,  94, 188, 101, 202, 137,  15,  30,  60, 120, 240, 253,
				231, 211, 187, 107, 214, 177, 127, 254, 225, 223, 163,  91, 182, 113, 226, 217,
				175,  67, 134,  17,  34,  68, 136,  13,  26,  52, 104, 208, 189, 103, 206, 129,
				31,  62, 124, 248, 237, 199, 147,  59, 118, 236, 197, 151,  51, 102, 204, 133,
				23,  46,  92, 184, 109, 218, 169,  79, 158,  33,  66, 132,  21,  42,  84, 168,
				77, 154,  41,  82, 164,  85, 170,  73, 146,  57, 114, 228, 213, 183, 115, 230,
				209, 191,  99, 198, 145,  63, 126, 252, 229, 215, 179, 123, 246, 241, 255, 227,
				219, 171,  75, 150,  49,  98, 196, 149,  55, 110, 220, 165,  87, 174,  65, 130,
				25,  50, 100, 200, 141,   7,  14,  28,  56, 112, 224, 221, 167,  83, 166,  81,
				162,  89, 178, 121, 242, 249, 239, 195, 155,  43,  86, 172,  69, 138,   9,  18,
				36,  72, 144,  61, 122, 244, 245, 247, 243, 251, 235, 203, 139,  11,  22,  44,
				88, 176, 125, 250, 233, 207, 131,  27,  54, 108, 216, 173,  71, 142,   1,   0
			};
		private byte[] log_table = new byte[]
			{
				0,   0,   1,  25,   2,  50,  26, 198,   3, 223,  51, 238,  27, 104, 199,  75,
				4, 100, 224,  14,  52, 141, 239, 129,  28, 193, 105, 248, 200,   8,  76, 113,
				5, 138, 101,  47, 225,  36,  15,  33,  53, 147, 142, 218, 240,  18, 130,  69,
				29, 181, 194, 125, 106,  39, 249, 185, 201, 154,   9, 120,  77, 228, 114, 166,
				6, 191, 139,  98, 102, 221,  48, 253, 226, 152,  37, 179,  16, 145,  34, 136,
				54, 208, 148, 206, 143, 150, 219, 189, 241, 210,  19,  92, 131,  56,  70,  64,
				30,  66, 182, 163, 195,  72, 126, 110, 107,  58,  40,  84, 250, 133, 186,  61,
				202,  94, 155, 159,  10,  21, 121,  43,  78, 212, 229, 172, 115, 243, 167,  87,
				7, 112, 192, 247, 140, 128,  99,  13, 103,  74, 222, 237,  49, 197, 254,  24,
				227, 165, 153, 119,  38, 184, 180, 124,  17,  68, 146, 217,  35,  32, 137,  46,
				55,  63, 209,  91, 149, 188, 207, 205, 144, 135, 151, 178, 220, 252, 190,  97,
				242,  86, 211, 171,  20,  42,  93, 158, 132,  60,  57,  83,  71, 109,  65, 162,
				31,  45,  67, 216, 183, 123, 164, 118, 196,  23,  73, 236, 127,  12, 111, 246,
				108, 161,  59,  82,  41, 157,  85, 170, 251,  96, 134, 177, 187, 204,  62,  90,
				203,  89,  95, 176, 156, 169, 160,  81,  11, 245,  22, 235, 122, 117,  44, 215,
				79, 174, 213, 233, 230, 231, 173, 232, 116, 214, 244, 234, 168,  80,  88, 175
			};
		private byte[] gen_poly = new byte[]
			{
				116, 231, 216,  30,   1,   0,   0,   0,   0,   
				0,   0,   0,   0,   0,   0,   0
			};
		private byte[] donotuse = new byte[]
			{0xff};

		#endregion
		private const int DECODED_DATA_SIZE = 28; 
		private const int ENCODED_DATA_SIZE = 32;
		private const int NUM_PARITY_BYTES  = (ENCODED_DATA_SIZE - DECODED_DATA_SIZE); 
		private const int SYNDROME_LENGTH   = NUM_PARITY_BYTES * 2;
		private const int POLY_SIZE         = NUM_PARITY_BYTES * 2; 
		private const int DOUBLE_POLY_SIZE  = POLY_SIZE * 2; 
		//unsigned char REEDSOLOMON[32];
		/// <summary>
		/// 入口字节数组
		/// </summary>
		public byte[] COMMONTEMP = new byte[1063];
		private static byte[] rstemp = new byte[52];

		private byte fast_mult(byte a,byte b)
		{
			if((a==0)||(b==0))
				return 0;
			else return exp_table[log_table[a] + log_table[b]];
		}
		private byte fast_inv(byte elt)
		{
			return exp_table[255 - log_table[elt]];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg_length"></param>
		public void encode_data(int msg_length)
		{
			int i;
			byte c, temp;
			byte[] msg = new byte[28];
			byte[] codeword = new byte[32];
			byte[] parity = new byte[4];
			for(i=0;i<msg_length;i++)msg[i]=COMMONTEMP[i];
			if (msg_length > 28)
			{
				return; //JUST CANT DO IT SO RETURN
			}
			for(i=0;i<msg_length;i++)codeword[i]=msg[i];
			for(i=0;i<4;i++)codeword[msg_length+i]=0;
			for(i=0;i<4;i++)parity[i]=0;
			// Calculate the parity bytes
			for (i = 0; i < msg_length; i++)
			{
				if ( (codeword[i] ^ parity[3]) != 0) 
				{
					c = log_table[codeword[i] ^ parity[3]];
					temp = exp_table[log_table[0x1E] + c];
					parity[3] = (byte)(parity[2]^temp);

					temp = exp_table[log_table[0xD8] + c];
					parity[2] = (byte)(parity[1]^temp);      	          	    
      	    
					temp = exp_table[log_table[0xE7] + c];
					parity[1] = (byte)(parity[0]^temp);  
			
					parity[0] = exp_table[log_table[0x74] + c];
				}
				else
				{
					parity[3] = (byte)(parity[2]^0);
					parity[2] = (byte)(parity[1]^0);
					parity[1] = (byte)(parity[0]^0);
					parity[0] = 0;
				}
		
			}
			// REORDER THE PARITY BYTES FROM  0, 1, 2, 3 --> 3, 2, 1, 0
			c = parity[0];
			parity[0] = parity[3];
			parity[3] = c;
			c = parity[1];
			parity[1] = parity[2];
			parity[2] = c;
			for(i=0;i<4;i++)//codeword[msg_length+i]=parity[i];
				//for(i=0;i<32;i++)REEDSOLOMON[i]=codeword[i];
				COMMONTEMP[msg_length+i] = parity[i];
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="codeword_len"></param>
		/// <returns></returns>
		public int decode_data(byte codeword_len)
		{
			int i, j, rc = 0;
			byte sum,temp,temp1;
			byte[] codeword = new byte[32];
			byte[] syndrome = new byte[8];
			for(i=0;i<codeword_len + 4;i++)codeword[i]=COMMONTEMP[i];
   
			for(i=0;i<8;i++)syndrome[i]=0;
			for (i = 0; i < 4; i++)
			{
				sum = 0;
				temp1 = exp_table[i+1];
				for (j = 0; j < (codeword_len+4); j++) 
				{
					if ( sum != 0)
					{
						temp = exp_table[log_table[temp1] + log_table[sum]];
					}
					else
					{
						temp = 0;
					}
					sum = (byte)(codeword[j] ^ temp);
				}
      
				rc += (syndrome[i] = sum);
			}
			for(i=0;i<8;i++)rstemp[i]=syndrome[i];
			return rc;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="codeword_len"></param>
		/// <returns></returns>
		public bool Do_Decode_Data(byte codeword_len)
		{
			if(decode_data(codeword_len) == 0)
			{
				return true;
			}
			if(decode_data(codeword_len) > 0)
			{
				return (correct_errors(codeword_len) == 0);
			}
			return true;
		}
		private byte compute_discrepancy(int L, int n)
		{
			int i;
			byte[] syndrome = new byte[8];
			byte[] lambda = new byte[8];
			byte sum=0;
			for (i = 0; i < 8; i++)lambda[i]=rstemp[28+i];
			for (i = 0; i < 8; i++)syndrome[i]=rstemp[i];	
			for (i = 0; i <= L; i++) 
			{
				sum ^= fast_mult(lambda[i], syndrome[n-i]);
			}

			return sum;
		}

		private void mult_polys()
		{
			int i, j, k;
			byte[] dst = new byte[16];
			byte[] p1 = new byte[8];
			byte[] p2 = new byte[8];
			byte[] tmp1 = new byte[16];
			for(i=0;i<16;i++)dst[i]=0;
			for(i=0;i<8;i++)p1[i]=rstemp[8+i];
			for(i=0;i<8;i++)p2[i]=rstemp[i];
			for (i = 0; i < 8; i++) 
			{
				for(k=0;k<8;k++)tmp1[8+k]=0;
				//       scale tmp1 by p1[i] 
				for(j = 0; j < 8; j++) 
				{
					tmp1[j] = fast_mult(p2[j], p1[i]);
				}

				//       and mult (shift) tmp1 right by i 
				for (j = 15; j >= i; j--) 
				{
					tmp1[j] = tmp1[j-i];
				}
				for(k=0;k<i;k++)tmp1[k]=0;

				//       add into partial product 
				for (j = 0; j < 16; j++) 
				{
					dst[j] ^= tmp1[j];
				}
			}
			for(i=0;i<16;i++)rstemp[36+i]=dst[i];
		}

		private void compute_modified_omega()
		{
			byte[] product = new byte[16];
			byte[] omega = new byte[8];
			int i;
			mult_polys();
			for(i=0;i<16;i++)product[i]=rstemp[36+i];
			for(i=0;i<8;i++)omega[i]=0;
			for(i=0;i<4;i++)omega[i]=product[i];
			for(i=0;i<8;i++)rstemp[16+i]=omega[i];
		}


		private void Modified_Berlekamp_Massey()
		{	
			int i, n, L = 0, L2, k = -1;
			byte j;
			byte[] lambda = new byte[8];
			byte d;
			byte[] psi = new byte[8];
			byte[] psi2 = new byte[8];
			byte[] D = new byte[8];
			for(i=0;i<8;i++)D[i]=0;
   
			D[0] = 1;
   
			for (i = 7; i > 0; i--)
			{
				D[i] = D[i-1];
			}
			D[0] = 0;
   
			for(i=0;i<8;i++)psi[i]=0;
   
			psi[0] = 1;

			for (n = 0; n < 4; n++)
			{
				for(j=0;j<8;j++)rstemp[28+j]=psi[j];
	  	
				d = compute_discrepancy(L, n);

				if (d != 0) 
				{		
					//          psi2 = psi - d*D 
					for (i = 0; i < 8; i++) 
					{
						psi2[i] = (byte)(psi[i] ^ fast_mult(d, D[i]));
					}

					if (L < (n-k)) 
					{
						L2 = n-k;
						k = n;
						for (i = 0; i < 4; i++) 
						{
							D[i] = fast_mult(psi[i], fast_inv(d));
						}
						L = L2;
					}
					for (i = 0; i < 8; i++)psi[i]=psi2[i];
				}

				for (i = 7; i > 0; i--)
				{
					D[i] = D[i-1];
				}
				D[0] = 0;
			}
			for (i = 0; i < 8; i++)lambda[i]=psi[i];
			for(i=0;i<8;i++)rstemp[8+i]=lambda[i];
			compute_modified_omega();
		}

		// given Psi (called Lambda in Modified_Berlekamp_Massey) and synBytes,
		// compute the combined erasure/error evaluator polynomial as Psi*S mod z^4


		private int Find_Roots()
		{
			int sum, i, j, errors = 0;	
			byte[] lambda = new byte[8];
			byte[] error_locations = new byte[4];
			for (i = 0; i < 8; i++)lambda[i]=rstemp[8+i];
			for (i = 1; i < 255; i++)
			{
				sum = 0;

				for (j = 0; j < 5; j++)
				{
					sum ^= fast_mult(exp_table[(j * i) % 255], lambda[j]);
				}

				if (sum == 0) 
				{ 
					error_locations[errors] = (byte)(255-i);
					errors++; 

					// Don't bother finding more errors then we can correct
					if (errors > 4)
					{
						break;
					}
				}
			}
			for (i = 0; i < 4; i++)rstemp[24+i]=error_locations[i];
			return errors;
		}

		private int correct_errors(byte codeword_len)
		{
			int r, i, j, err, num_errors;
			byte num, denom;
			byte[] codeword = new byte[32];
			byte[] syndrome = new byte[8];
			byte[] error_locations = new byte[4];
			byte[] lambda = new byte[8];
			byte[] omega = new byte[8];
			for(i=0;i<32;i++)codeword[i]=COMMONTEMP[i];
			for(i=0;i<8;i++)syndrome[i]=rstemp[i];
   
			Modified_Berlekamp_Massey();
			for(i=0;i<8;i++)lambda[i]=rstemp[8+i];
			for(i=0;i<8;i++)omega[i]=rstemp[16+i];
			num_errors = Find_Roots();
			for(i=0;i<4;i++)error_locations[i]=rstemp[24+i];
			if (num_errors > 4) return 1;

			for (r = 0; r < num_errors; r++) 
			{
				//       first check for illegal error locs 
				if (error_locations[r] >= (codeword_len+4))  // Modified from DECODED_DATA_SIZE
				{
					for(i=0;i<32;i++)COMMONTEMP[i]=codeword[i];
					return 1;
				}

				i = error_locations[r];

				//       evaluate Omega at alpha^(-i) 
				num = 0;
				for (j = 0; j < 8; j++) 
				{
					num ^= fast_mult(omega[j], exp_table[((255 - i) * j) % 255]);
				}

				//       evaluate Lambda' (derivative) at alpha^(-i) ; all odd powers disappear 
				denom = 0;
				for (j = 1; j < 8; j += 2) 
				{
					denom ^= fast_mult(lambda[j], exp_table[((255 - i ) * (j - 1)) & 0xFF]);
				}

				err = fast_mult(num, fast_inv(denom));
				codeword[codeword_len - i +3] = (byte)(codeword[codeword_len - i +3] ^ err);
				//codeword[codeword_len - i +3] ^= err;
			}
			for(i=0;i<32;i++)COMMONTEMP[i]=codeword[i];    
			return 0;
		}

	}
}
