<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  
      xmlns:n="http://www.portalfiscal.inf.br/nfe"
      xmlns:date="http://exslt.org/formatoData"
      xmlns:chave="http://exslt.org/chaveacesso" 
      xmlns:r="http://www.serpro.gov.br/nfe/remessanfe.xsd"
      exclude-result-prefixes="date" >
  <xsl:decimal-format decimal-separator="," grouping-separator="." />
  <xsl:include href="Utils.xsl"/> 
 <xsl:template match="/">
 <html>
<head>
<!-- <link rel="stylesheet" type="text/css" href="D:/Projetos/NFCe/src.emulador.nfce/nfce/css/sefaz_nfce.css"/> -->
<link rel="stylesheet" type="text/css" href="css/sefaz_nfce.css"/>
<meta http-equiv="Content-type" content="text/html;charset=UTF-8"/>
</head>
<body>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center">
	<table width="500" border="1" cellspacing="0" cellpadding="0" bordercolor="#0D000A"  bgcolor="#FFFFEA">
	  <tr>
		<td>			
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
			  <tr>
				<td class="borda-pontilhada-botton">
					<!-- <img id="imgInvalido" class="imagem-cancelado" src="nfce/images/logo.png" alt="NFC-e" align="middle" width="261" height="63" /> -->
					<table class="NFCCabecalho" border="0">				
						<tr>
							<td rowspan="4" align="left"><img src="images/logoMarcaNFC.PNG" alt="NFC-e" width="80" height="50" /></td>
							<td class="NFCCabecalho_Titulo NFCBold" align="left"><xsl:value-of select="//n:infNFe/n:emit/n:xNome" />  [ BENEFIX SISTEMAS ]</td>
						</tr>						
						<tr>
							<td class="NFCCabecalho_SubTitulo1" align="left">
								<xsl:variable name="cnpjEmit" select="//n:infNFe/n:emit/n:CNPJ"/>
							CNPJ:
								<xsl:call-template name="formatCnpj">
									<xsl:with-param name="cnpj" select="$cnpjEmit"/>
								</xsl:call-template><xsl:value-of select="concat('   ','   ')" />
							Inscrição Estadual: <xsl:value-of select="//n:infNFe/n:emit/n:IE" />
							</td>
						</tr>
						<xsl:if test="string-length(//n:infNFe/n:emit/n:IM) != 0">
						<tr>
							<td class="NFCCabecalho_SubTitulo1" align="left">							
							Inscrição Municipal: <xsl:value-of select="//n:infNFe/n:emit/n:IM" />
							</td>
						</tr>
						</xsl:if>
					</table>
					<table class="NFCCabecalho" border="0">
						<tr>
							<td class="NFCCabecalho_SubTitulo1" align="left">
								<xsl:value-of select="//n:infNFe/n:emit/n:enderEmit/n:xLgr" />, 
								<xsl:value-of select="//n:infNFe/n:emit/n:enderEmit/n:nro" />,
								<xsl:value-of select="//n:infNFe/n:emit/n:enderEmit/n:xBairro" />,
								<xsl:value-of select="//n:infNFe/n:emit/n:enderEmit/n:xMun" />,
								<xsl:value-of select="//n:infNFe/n:emit/n:enderEmit/n:UF" />
							</td>
						</tr>
					</table>
				</td>
			  </tr>
			  <tr>
				<td>
					<table class="NFCCabecalho" border="0">
						<tr>
							<td class="NFCCabecalho_Titulo NFCBold" align="center">DANFE NFC-e - Documento Auxiliar da Nota Fiscal Eletrônica para Consumidor Final</td>				
						</tr>											
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">NFC-e não permite aproveitamento de crédito de ICMS</td>
						</tr>						
					</table>
				</td>
			  </tr>
			  
			  <tr>
				<td class="borda-pontilhada-botton">
					<table class="NFCCabecalho" border="0">
						<tr>
							<td class="borda-pontilhada-3D NFCDetalhe_Item" align="left" style="width: 60px;">Código</td>
							<td class="borda-pontilhada-3D NFCDetalhe_Item" align="left" style="width: 200px;">Descrição</td>
							<td class="borda-pontilhada-3D NFCDetalhe_Item" align="center" style="width: 50px;">Qtde</td>
							<td class="borda-pontilhada-3D NFCDetalhe_Item" align="left" style="width: 10px;">Un</td>
							<td class="borda-pontilhada-3D NFCDetalhe_Item" align="right" style="width: 60px;">Vl Unit</td>
							<xsl:if test="string-length(//n:infNFe/n:total/n:ICMSTot/n:vTotTrib) != 0">
								  <td class="borda-pontilhada-3D NFCDetalhe_Item" align="right" style="width: 60px;">Vl Tributo</td>
							</xsl:if>
							<td class="borda-pontilhada-top-botton" align="right" style="width: 60px;">Vl Total</td>
						</tr>
						<xsl:for-each select="//n:infNFe/n:det">
						<xsl:variable name="chaves" select="@nItem"/>
						<xsl:variable name="nrIDSeq" select="concat('Item',' + ',$chaves)"/>				
							<tr id="{$nrIDSeq}">					
								<td class="NFCDetalhe_Item" align="left" style="width: 60px;"><xsl:value-of select="n:prod/n:cProd" /></td>						
								<td class="NFCDetalhe_Item" align="left" style="width: 300px;"><xsl:value-of select="n:prod/n:xProd" /></td>						
								<td class="NFCDetalhe_Item" align="center" style="width: 50px;"><xsl:value-of select="format-number(n:prod/n:qCom,'0,####')" /></td>						
								<td class="NFCDetalhe_Item" align="left" style="width: 10px;"><xsl:value-of select="n:prod/n:uCom" /></td>						
								<td class="NFCDetalhe_Item" align="right" style="width: 70px;"><xsl:value-of select="format-number(n:prod/n:vUnCom,'##.##.##0,##########')" /></td>
								<xsl:if test="string-length(//n:infNFe/n:total/n:ICMSTot/n:vTotTrib) != 0">
								  <td class="NFCDetalhe_Item" align="right" style="width: 70px;">									
									<xsl:call-template name="format2Casas">
									  <xsl:with-param name="num" select="n:imposto/n:vTotTrib"/>									  
									</xsl:call-template>
								  </td>
								</xsl:if>
								<td class="NFCDetalhe_Item" align="right" style="width: 70px;"><xsl:value-of select="format-number(n:prod/n:vProd,'##.##.##0,00')" /></td>						
							</tr>					
						</xsl:for-each>
					</table>
				</td>
			  </tr>
			  <tr>
				<td class="borda-pontilhada-botton">
					<table class="NFCCabecalho" border="0">
						<tr>
							<td class="NFCDetalhe_Item" align="left" style="width: 410px;">Qtd. Total de Itens</td>
							<td class="NFCDetalhe_Item" align="right" style="width: 70px;"><xsl:value-of select="count(//n:infNFe/n:det)"/></td>					
						</tr>
						<tr>
							<td class="NFCDetalhe_Item" align="left" style="width: 410px;">Valor Total R$</td>
							<td class="NFCDetalhe_Item" align="right" style="width: 70px;"><xsl:value-of select="format-number(//n:infNFe/n:total/n:ICMSTot/n:vNF,'##.##.##0,00')" /></td>					
						</tr>
						<tr>
							<td class="NFCDetalhe_Item" align="left" style="width: 410px;">Valor Descontos R$</td>
							<td class="NFCDetalhe_Item" align="right" style="width: 70px;"><xsl:value-of select="format-number(//n:infNFe/n:total/n:ICMSTot/n:vDesc,'##.##.##0,00')" /></td>					
						</tr>
						<tr>
							<td class="NFCDetalhe_Item" align="left" style="width: 410px;">FORMA PAGAMENTO</td>	
							<td class="NFCDetalhe_Item" align="right" style="width: 120px;">VALOR PAGO</td>
						</tr>						
						<xsl:for-each select="//n:infNFe/n:pag">
							<tr>
								<td class="NFCDetalhe_Item" align="left" style="width: 410px;">
									<xsl:variable name="tpPgto" select="n:tPag"/>
									<xsl:if test="$tpPgto='01'">Dinheiro</xsl:if>
									<xsl:if test="$tpPgto='02'">Cheque</xsl:if>
									<xsl:if test="$tpPgto='03'">Cartão de Crédito</xsl:if>
									<xsl:if test="$tpPgto='04'">Cartão de Débito</xsl:if>
									<xsl:if test="$tpPgto='05'">Crédito Loja</xsl:if>									
									<xsl:if test="$tpPgto='10'">Vale Alimentação</xsl:if>
									<xsl:if test="$tpPgto='11'">Vale Refeição</xsl:if>
									<xsl:if test="$tpPgto='12'">Vale Presente</xsl:if>
									<xsl:if test="$tpPgto='13'">Vale Combustível</xsl:if>
									<xsl:if test="$tpPgto='99'">Outros</xsl:if>
								</td>
								<td class="NFCDetalhe_Item" align="right" style="width: 70px;"><xsl:value-of select="format-number(n:vPag,'##.##.##0,00')" /></td>							
							</tr>				
						</xsl:for-each>
						<xsl:for-each select ="//n:infNFe/n:total/n:ICMSTot/n:vTotTrib">
						  <tr>
							<td class="borda-pontilhada-top NFCDetalhe_Item" align="left" style="width: 780px;">Valor Total de Tributos (Lei 12.741/12)R$</td>
							<td class="borda-pontilhada-top NFCDetalhe_Item" align="right" style="width: 70px;"><xsl:value-of select="format-number(//n:infNFe/n:total/n:ICMSTot/n:vTotTrib,'##.##.##0,00')" /></td>
						  </tr>
						</xsl:for-each>						
					</table>
				</td>
			  </tr>	
			  				
			  <tr>
				<td>
					<table border="0" style="width: 100%;">
						<xsl:variable name="tpAmb" select="//n:infNFe/n:ide/n:tpAmb"/>		
						<xsl:if test="$tpAmb='2'">
						<tr>
							<td class="NFCCabecalho_SubTitulo NFCBold" align="center">
								EMITIDA EM AMBIENTE DE HOMOLOGAÇÃO - SEM VALOR FISCAL						
							</td>				
						</tr>
						</xsl:if>
						<tr>
							<td class="NFCCabecalho_SubTitulo NFCBold" align="center">
								<xsl:variable name="tpEmis" select="//n:infNFe/n:ide/n:tpEmis"/>						
								<xsl:if test="$tpEmis='1'">EMISSÃO NORMAL</xsl:if>
								<xsl:if test="$tpEmis!='1'">EMITIDA EM CONTINGÊNCIA</xsl:if>
							</td>				
						</tr>	
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">
								Número: <xsl:value-of select="//n:infNFe/n:ide/n:nNF" />
								Série: <xsl:value-of select="//n:infNFe/n:ide/n:serie" />
								<xsl:variable name="dhEmi" select="//n:infNFe/n:ide/n:dhEmi"/> 
								Data de Emissão: <xsl:call-template name="formatDateTime">
								  <xsl:with-param name="dateTime" select="$dhEmi"/>
								  <xsl:with-param name="include_as" select="true"/>
								</xsl:call-template>  - Via Consumidor					
							</td>
						</tr>											
						<xsl:variable name="ufEmit" select="//n:infNFe/n:emit/n:enderEmit/n:UF"/>
						<xsl:if test="$ufEmit='AC'">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">Consulte pela Chave de Acesso em http://www.sefaznet.ac.gov.br/nfe</td>
						</tr>
						</xsl:if>
						<xsl:if test="$ufEmit='AM'">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">Consulte pela Chave de Acesso em http://sistemas.sefaz.am.gov.br/nfceweb</td>
						</tr>
						</xsl:if>
						<xsl:if test="$ufEmit='MA'">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">Consulte pela Chave de Acesso em http://www.nfe.ma.gov.br/portal</td>
						</tr>
						</xsl:if>
						<xsl:if test="$ufEmit='MT'">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">Consulte pela Chave de Acesso em http://www.nfe.sefaz.mt.gov.br/portal</td>
						</tr>
						</xsl:if>
						<xsl:if test="$ufEmit='RN'">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">Consulte pela Chave de Acesso em http://www.nfe.rn.gov.br/portal</td>
						</tr>
						</xsl:if>
						<xsl:if test="$ufEmit='RS'">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">Consulte pela Chave de Acesso em https://www.sefaz.rs.gov.br/NFCE</td>
						</tr>
						</xsl:if>
						<xsl:if test="$ufEmit='SE'">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">Consulte pela Chave de Acesso em http://www.nfe.se.gov.br/portal</td>
						</tr>
						</xsl:if>
						<tr>
							<td class="NFCCabecalho_SubTitulo NFCBold" align="center">CHAVE DE ACESSO</td>
						</tr>
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center">
								<xsl:call-template name="formatNfe">
									<xsl:with-param name="nfe" select="substring(//n:infNFe/@Id, 4)"/>
								</xsl:call-template>
							</td>
						</tr>						
					</table>
				</td>
			  </tr>			  
			  <tr>
				<td class="borda-pontilhada-top" >
					<table border="0" style="width: 100%;">
						<tr>
							<td class="NFCCabecalho_SubTitulo NFCBold" align="center" width="290" >CONSUMIDOR</td>													
						</tr>						
						<xsl:if test="//n:infNFe/n:dest/n:CNPJ!=''">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center" width="300">
								<xsl:variable name="cnpjDest" select ="//n:infNFe/n:dest/n:CNPJ"/>
								<xsl:variable name="cpfDest" select ="//n:infNFe/n:dest/n:CPF"/>	
								<xsl:variable name="nmDest" select ="//n:infNFe/n:dest/n:xNome"/>
								CNPJ:
								<xsl:call-template name="formatCnpj">
									<xsl:with-param name="cnpj" select="$cnpjDest"/>
								</xsl:call-template>
								<xsl:if test="$nmDest!=''">
									<xsl:value-of select="concat(' - ', $nmDest)"/>									
								</xsl:if>
							</td>
						</tr>
						</xsl:if>
						<xsl:if test="//n:infNFe/n:dest/n:CPF!=''">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center" width="300">
								<xsl:variable name="cnpjDest" select ="//n:infNFe/n:dest/n:CNPJ"/>
								<xsl:variable name="cpfDest" select ="//n:infNFe/n:dest/n:CPF"/>	
								<xsl:variable name="nmDest" select ="//n:infNFe/n:dest/n:xNome"/>
								CPF:
								<xsl:call-template name="formatCpf">
								  <xsl:with-param name="cpf" select="$cpfDest"/>
								</xsl:call-template>	
								<xsl:if test="$nmDest!=''">
									<xsl:value-of select="concat(' - ', $nmDest)"/>
								</xsl:if>
							</td>
						</tr>
						</xsl:if>
						<xsl:if test="//n:infNFe/n:dest/n:idEstrangeiro!=''">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center" width="300">
								<xsl:variable name="cnpjDest" select ="//n:infNFe/n:dest/n:CNPJ"/>
								<xsl:variable name="cpfDest" select ="//n:infNFe/n:dest/n:CPF"/>						
								<xsl:variable name="nmDest" select ="//n:infNFe/n:dest/n:xNome"/>
								ID Estrangeiro:
								<xsl:value-of select="//n:infNFe/n:dest/n:idEstrangeiro" />
								<xsl:if test="$nmDest!=''">
									<xsl:value-of select="concat(' - ', $nmDest)"/>
								</xsl:if>
							</td>
						</tr>
						</xsl:if>
						<xsl:if test="not(//n:infNFe/n:dest/n:CNPJ) and not(//n:infNFe/n:dest/n:CPF) and not(//n:infNFe/n:dest/n:idEstrangeiro)">
						<tr>
							<td class="NFCCabecalho_SubTitulo" align="center" width="300">
								<xsl:variable name="cnpjDest" select ="//n:infNFe/n:dest/n:CNPJ"/>
								<xsl:variable name="cpfDest" select ="//n:infNFe/n:dest/n:CPF"/>						
								Consumidor não identificado
							</td>
						</tr>
						</xsl:if>
						<xsl:if test="//n:infNFe/n:dest/n:enderDest/n:xLgr!=''">
						<tr>
							<td class="NFCCabecalho NFCCabecalho_SubTitulo1" align="center">
								<xsl:value-of select="//n:infNFe/n:dest/n:enderDest/n:xLgr" />, 
								<xsl:value-of select="//n:infNFe/n:dest/n:enderDest/n:nro" />,
								<xsl:value-of select="//n:infNFe/n:dest/n:enderDest/n:xBairro" />,
								<xsl:value-of select="//n:infNFe/n:dest/n:enderDest/n:xMun" />,
								<xsl:value-of select="//n:infNFe/n:dest/n:enderDest/n:UF" />
							</td>
						</tr>
						</xsl:if>
					</table>				  
				</td>
			  </tr>			  
			  <tr>
				<td class="borda-pontilhada-top NFCCabecalho_SubTitulo" align="center">
					Consulta via leitor de QR Code: 
				</td>
				</tr>
				<tr>
				<td align="center"><img src="images/qrcode.png" alt="QRCode" vspace="35px" /></td>				
			  </tr>
			  <tr>				
				<td class="borda-pontilhada-top NFCDetalhe_Item " align="left">
					<table border="0" style="width: 100%;">
						<tr>
							<td class="NFCDetalhe_Item" align="left" style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; FONT-SIZE: 10px;">
								<xsl:variable name="tpAmbiente" select="//n:infNFe/n:ide/n:tpAmb"/>								
								<xsl:if test="$tpAmbiente='1'"> <strong>Ambiente de Produção</strong> </xsl:if>
								<xsl:if test="$tpAmbiente='2'"> <strong>Ambiente de Homologação</strong> </xsl:if>					
							</td>
							<td class="NFCDetalhe_Item" align="left" style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; FONT-SIZE: 10px;">
								<strong>Versão XML: <xsl:value-of select="//n:NFe/n:infNFe/@versao"/></strong>								
						    </td>
							<td class="NFCDetalhe_Item " align="right" style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; FONT-SIZE: 10px;">
								<strong>Versão XSLT: 1.12</strong>
							</td>													
						</tr>
					</table>
				</td>
				
				
			  </tr>	
			</table>	
		</td>
	  </tr>
	</table>
	
   </td>
  </tr>
</table>
 </body>
</html>
  </xsl:template>
</xsl:stylesheet>