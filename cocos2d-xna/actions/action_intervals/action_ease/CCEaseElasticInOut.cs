/****************************************************************************
Copyright (c) 2010-2012 cocos2d-x.org
Copyright (c) 2008-2009 Jason Booth
Copyright (c) 2011-2012 Fulcrum Mobile Network, Inc

http://www.cocos2d-x.org
http://www.openxlive.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace cocos2d.actions.action_intervals.action_ease
{
    public class CCEaseElasticInOut : CCEaseElastic
    {
        public override void update(float time) 
        {
            float newT = 0;

            if (time == 0 || time == 1)
            {
                newT = time;
            }
            else
            {
                time = time * 2;
                if (m_fPeriod==0)
                {
                    m_fPeriod = 0.3f * 1.5f;
                }

                float s = m_fPeriod / 4;

                time = time - 1;
                if (time < 0)
                {
                    newT = (float)(-0.5f * Math.Pow(2, 10 * time) * Math.Sin((time - s) * MathHelper.TwoPi/ m_fPeriod));
                }
                else
                {
                    newT = (float)(Math.Pow(2, -10 * time) * Math.Sin((time - s) * MathHelper.TwoPi / m_fPeriod) * 0.5f + 1);
                }
            }

            m_pOther.update(newT);
        }

        public override CCFiniteTimeAction reverse() 
        {
            return CCEaseInOut.actionWithAction((CCActionInterval)m_pOther.reverse(), m_fPeriod);
        }

        public override CCObject copyWithZone(CCZone pZone) 
        {
            CCZone pNewZone = null;
            CCEaseElasticInOut pCopy = null;

            if (pZone != null && pZone.m_pCopyObject != null)
            {
                //in case of being called at sub class
                pCopy = pZone.m_pCopyObject as CCEaseElasticInOut;
            }
            else
            {
                pCopy = new CCEaseElasticInOut();
                pZone = pNewZone = new CCZone(pCopy);
            }

            pCopy.initWithAction((CCActionInterval)(m_pOther.copy()), m_fPeriod);

            return pCopy;
        }

	    /// <summary>
         /// creates the action
	    /// </summary>
	    /// <param name="pAction"></param>
	    /// <returns></returns>
        public new static CCEaseElasticInOut actionWithAction(CCActionInterval pAction) 
        {
            CCEaseElasticInOut pRet = new CCEaseElasticInOut();

            if (pRet != null)
            {
                if (pRet.initWithAction(pAction))
                {
                    //pRet->autorelease();
                }
                else
                {
                    //CC_SAFE_RELEASE_NULL(pRet);
                }
            }

            return pRet; 
        }

	    /// <summary>
        /// Creates the action with the inner action and the period in radians (default is 0.3)
	    /// </summary>
	    /// <param name="pAction"></param>
	    /// <param name="fPeriod"></param>
	    /// <returns></returns>
        public new static CCEaseElasticInOut actionWithAction(CCActionInterval pAction, float fPeriod) 
        {
            CCEaseElasticInOut pRet = new CCEaseElasticInOut();

            if (pRet != null)
            {
                if (pRet.initWithAction(pAction, fPeriod))
                {
                    //pRet->autorelease();
                }
                else
                {
                    //CC_SAFE_RELEASE_NULL(pRet);
                }
            }

            return pRet; 
        }
    }
}
