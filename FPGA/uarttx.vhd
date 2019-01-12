library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;
--Uart发送模块
--clk为对应的波特率时钟
--发送数据时，给wrsig一个上升沿同时往datain里面塞入待发送的数据
--tx为发送接收端
entity uarttx is
	port(clk:in std_logic;
		rst_n:in std_logic;
		datain:in std_logic_vector(7 downto 0);
		wrsig:in std_logic;
		idle:out std_logic;
		tx: out std_logic);
end uarttx;

architecture behavioral of uarttx is
signal send:std_logic;							--发送标志位
signal wrsigbuf,wrsigrise:std_logic;		--用于检测发送触发信号的上升沿
signal idletemp:std_logic;
signal cnt:integer range 0 to 255;
begin
process(clk)
begin
	if(rising_edge(clk))then
		wrsigbuf <= wrsig;
		wrsigrise <= (not wrsigbuf) and wrsig;  
	end if;
end process;
process(clk)
begin
	if((wrsigrise and (not idletemp)) = '1')then
		send <= '1';
	elsif(cnt = 160)then
		send <= '0';
	end if;
end process;
process(clk,rst_n)
begin
	if(rst_n = '0')then
			tx <= '0';
         idletemp <= '0';
			cnt<= 0;
	elsif(rising_edge(clk))then
		if(send = '1')then
			case cnt is
				when 0 =>tx <= '0';idletemp <= '1';cnt <= cnt + 1;
				when 16 =>tx <= datain(0);idletemp <= '1';cnt <= cnt + 1;
				when 32 =>tx <= datain(1);idletemp <= '1';cnt <= cnt + 1;
				when 48 =>tx <= datain(2);idletemp <= '1';cnt <= cnt + 1;
				when 64 =>tx <= datain(3);idletemp <= '1';cnt <= cnt + 1;
				when 80 =>tx <= datain(4);idletemp <= '1';cnt <= cnt + 1;
				when 96 =>tx <= datain(5);idletemp <= '1';cnt <= cnt + 1;
				when 112 =>tx <= datain(6);idletemp <= '1';cnt <= cnt + 1;
				when 128 =>tx <= datain(7);idletemp <= '1';cnt <= cnt + 1;
				when 144 =>tx <= '1';idletemp <= '1';cnt <= cnt + 1;
				when 160 =>tx <= '1';idletemp <= '0';cnt <= cnt + 1;
				when others =>cnt <= cnt + 1;
			end case;
		else	
			tx <= '1';
			cnt <= 0;
			idletemp <= '0';
		end if;
	end if;
end process;
idle <= idletemp;
end behavioral;